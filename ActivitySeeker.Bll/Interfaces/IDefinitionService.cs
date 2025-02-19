using ActivitySeeker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
    }
}
