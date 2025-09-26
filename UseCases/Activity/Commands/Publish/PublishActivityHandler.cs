namespace UseCases.Activity.Commands.Publish
{
    using MediatR;
    using Interfaces.Repos;
    internal class PublishActivityHandler : IRequestHandler<PublishActivityCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActivityRepository _repository;
        public PublishActivityHandler(IActivityRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task Handle(PublishActivityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            entity.PublishActivity();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
