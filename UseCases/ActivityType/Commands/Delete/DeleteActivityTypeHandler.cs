namespace UseCases.ActivityType.Commands.Delete
{
    using MediatR;
    using Interfaces.Repos;
    internal class DeleteActivityTypeHandler : IRequestHandler<DeleteActivityTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActivityTypeRepository _repository;

        public DeleteActivityTypeHandler(IActivityTypeRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task Handle(DeleteActivityTypeCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.ActivityTypeId, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
