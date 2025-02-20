using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Interfaces
{
    public interface IDefinitionService
    {
        Task CreateState(StateEntity stateEntity);
        Task CreateTransition(TransitionEntity transitionEntity);
        Task<IEnumerable<StateEntity>> GetStates();
        Task<IEnumerable<TransitionEntity>> GetTransitions();
        Task UpdateState(StateEntity toEntity);
        Task UpdateTransition(TransitionEntity toEntity);
        Task<TransitionEntity> GetTransitionByName(string transitionName);
    }
}
