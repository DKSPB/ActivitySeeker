using MediatR;
using UseCases.Interfaces.Image;
using UseCases.Activity.Queries.GetImage.Models;
using UseCases.Interfaces.Repos;

namespace UseCases.Activity.Commands.UploadImage
{
    internal class UploadActivityImageHandler : IRequestHandler<UploadActivityImageCommand>
    {
        private readonly IActivityRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileValidator _validator;
        private readonly IImageVariantGenerator _imageGenerator;
        private readonly IFileStorage _fileStorage;
    
        public UploadActivityImageHandler(IActivityRepository repository, IUnitOfWork unitOfWork, IFileValidator validator, IFileStorage fileStorage,IImageVariantGenerator imageGenerator)
        {
            _repository = repository;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _fileStorage = fileStorage;
            _imageGenerator = imageGenerator;
            
        }
        public async Task Handle(UploadActivityImageCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            var fileData = await _validator.ValidateAndGetStreamAsync(request.InputFile, cancellationToken);
        
            var fileName = _fileStorage.GenerateUniqueFileName(fileData.FileExtension);
         
            var images = await _imageGenerator.GenerateAsync(fileData.Content, fileData.FileExtension);

            await _fileStorage.SaveAsync(images.Small, _fileStorage.GetImagePath(ImageSize.Small), fileName, cancellationToken);

            await _fileStorage.SaveAsync(images.Medium, _fileStorage.GetImagePath(ImageSize.Medium), fileName, cancellationToken);
        
            await _fileStorage.SaveAsync(images.Original, _fileStorage.GetImagePath(ImageSize.Original), fileName, cancellationToken);
        
            entity.ImageName = fileName;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}