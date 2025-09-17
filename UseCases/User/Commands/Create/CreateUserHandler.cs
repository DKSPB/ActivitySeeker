namespace UseCases.User.Commands.Create
{
    using MediatR;
    using Interfaces.Repos;
    using Domain.Entities;

    internal class CreateUserHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _repository;
        public CreateUserHandler(IUserRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new User { Id = command.Id}, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
