using MediatR;
using UseCases.Common;
using UseCases.Interfaces;
using DataAccess.Interfaces;

namespace UseCases.Activity.Commands.UploadImage;

internal class UploadActivityImageHandler : IRequestHandler<UploadActivityImageCommand>
{
    private readonly IDbContext _context;
    private readonly IFileValidator _validator;
    private readonly IImageVariantGenerator _imageGenerator;
    private readonly IFileStorage _fileStorage;
    
    public UploadActivityImageHandler(IDbContext context, IFileValidator validator, IFileStorage fileStorage, IImageVariantGenerator imageGenerator)
    {
        _context = context;
        _validator = validator;
        _imageGenerator = imageGenerator;
        _fileStorage = fileStorage;
    }
    public async Task Handle(UploadActivityImageCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Activities
            .FindAsync(new object?[] { request.ActivityId }, cancellationToken);

        if (entity is null)
        {
            throw new ObjectNotFoundException(nameof(Domain.Entities.Activity), request.ActivityId);
        }

        var fileData = await _validator.ValidateAndGetStreamAsync(request.InputFile, cancellationToken);
         
        var images = await _imageGenerator.GenerateAsync(fileData.Content, fileData.FileExtension);

        var smallFileName = await _fileStorage.SaveAsync(images.Small, fileData.FileExtension, cancellationToken);

        var mediumFileName = await _fileStorage.SaveAsync(images.Medium, fileData.FileExtension, cancellationToken);
        
        //var fileName = await _fileStorage.SaveAsync(request.File.Content, request.File.FileExtension, cancellationToken);
        
        //entity.ImagePath = fileName;

        await _context.SaveChangesAsync(cancellationToken);
    }
}