namespace UseCases.Activity.Queries.GetImage
{
    using Models;
    using MediatR;
    using Interfaces.Image;
    using Interfaces.Repos;
    public class GetImageHandler : IRequestHandler<GetImageCommand, FileResult?>
    {
        private readonly IActivityRepository _repository;
        private readonly IFileStorage _fileStorage;

        public GetImageHandler(IActivityRepository repository, IFileStorage fileStorage)
        {
            _repository = repository;
            _fileStorage = fileStorage;
        }
    
        public async Task<FileResult?> Handle(GetImageCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.ActivityId, cancellationToken);

            if (entity.ImageName is null) 
                throw new FileNotFoundException($"У активности {request.ActivityId} нет изображения");

            var imagePath = _fileStorage.GetImagePath(request.ImageSize);

            var imageFullPath = Path.Combine(imagePath, entity.ImageName);
        
            return entity.ImageName is null ? null : 
                new FileResult 
                { 
                    Extension = Path.GetExtension(imageFullPath), 
                    Content = await _fileStorage.GetAsync(imageFullPath) 
                };
        }
    }
}