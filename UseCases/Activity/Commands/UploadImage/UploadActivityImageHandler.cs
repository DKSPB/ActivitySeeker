using MediatR;
using UseCases.Common;
using UseCases.Interfaces.Image;
using DataAccess.Interfaces;
using UseCases.Activity.Queries.GetImage.Models;

namespace UseCases.Activity.Commands.UploadImage;

internal class UploadActivityImageHandler : IRequestHandler<UploadActivityImageCommand>
{
    private readonly IDbContext _context;
    private readonly IFileValidator _validator;
    private readonly IImageVariantGenerator _imageGenerator;
    private readonly IFileStorage _fileStorage;
    
    public UploadActivityImageHandler(IDbContext context, IFileValidator validator, IFileStorage fileStorage,IImageVariantGenerator imageGenerator)
    {
        _context = context;
        _validator = validator;
        _imageGenerator = imageGenerator;
        _fileStorage = fileStorage;
    }
    public async Task Handle(UploadActivityImageCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Activities
            .FindAsync(new object?[] { request.Id }, cancellationToken) ?? 
            throw new ObjectNotFoundException(nameof(Domain.Entities.Activity), request.Id);

        var fileData = await _validator.ValidateAndGetStreamAsync(request.InputFile, cancellationToken);
        
        var fileName = _fileStorage.GenerateUniqueFileName(fileData.FileExtension);
         
        var images = await _imageGenerator.GenerateAsync(fileData.Content, fileData.FileExtension);

        await _fileStorage.SaveAsync(images.Small, _fileStorage.GetImagePath(ImageSize.Small), fileName, cancellationToken);

        await _fileStorage.SaveAsync(images.Medium, _fileStorage.GetImagePath(ImageSize.Medium), fileName, cancellationToken);
        
        await _fileStorage.SaveAsync(images.Original, _fileStorage.GetImagePath(ImageSize.Original), fileName, cancellationToken);
        
        entity.ImageName = fileName;

        await _context.SaveChangesAsync(cancellationToken);
    }
}