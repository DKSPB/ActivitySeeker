using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain;
using ActivitySeeker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ActivitySeeker.Bll.Services
{
    public class DefinitionService : IDefinitionService
    {
        private readonly ActivitySeekerContext _context;

        public DefinitionService(ActivitySeekerContext context)
        {
            _context = context;
        }

        public async Task CreateState(StateEntity stateEntity)
        {
            await _context.States.AddAsync(stateEntity);
            await _context.SaveChangesAsync();
        }

        public async Task CreateTransition(TransitionEntity transitionEntity)
        {
            await _context.Transitions.AddAsync(transitionEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StateEntity>> GetStates()
        {
            return await _context.States.ToListAsync();
        }

        public async Task<IEnumerable<TransitionEntity>> GetTransitions()
        {
            return await _context.Transitions.ToListAsync();
        }

        public Task UpdateState(StateEntity toEntity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTransition(TransitionEntity toEntity)
        {
            throw new NotImplementedException();
        }
    }
}
