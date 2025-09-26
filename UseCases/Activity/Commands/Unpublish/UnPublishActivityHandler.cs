namespace UseCases.Activity.Commands.UnPublish
{
    using MediatR;
    using Interfaces.Repos;
    internal class UnPublishActivityHandler : IRequestHandler<UnPublishActivityCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActivityRepository _repository;
        public UnPublishActivityHandler(IActivityRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public async Task Handle(UnPublishActivityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            entity.UnpublishActivity();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
    }
}
