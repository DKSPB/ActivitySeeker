using AutoMapper;
using MediatR;
using UseCases.Interfaces.Repos;
using UseCases.User.Models;

namespace UseCases.User.Commands.EnsureUserExists;

internal class EnsureUserExistsHandler : IRequestHandler<EnsureUserExistsCommand, (bool, UserDto)>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _repository;

    public EnsureUserExistsHandler(IMapper mapper, IUserRepository repository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }
        
    public async Task<(bool, UserDto)> Handle(EnsureUserExistsCommand command, CancellationToken cancellationToken)
    {
        var entity = await _repository.FindAsync(command.UserId, cancellationToken);
        
        return entity is null ? (true, await CreateUser(command.UserId, cancellationToken)) :
            (false, new UserDto{ Id = entity.Id});
    }

    private async Task<UserDto> CreateUser(long userId, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.User { Id = userId };
        
        await _repository.CreateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new UserDto {Id = entity.Id};
    }
}