namespace UseCases.Activity.Commands.Delete
{
    using MediatR;
    using Interfaces.Repos;
    
    public class DeleteActivityHandler : IRequestHandler<DeleteActivityCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActivityRepository _repository;

        public DeleteActivityHandler(IActivityRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public async Task Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}