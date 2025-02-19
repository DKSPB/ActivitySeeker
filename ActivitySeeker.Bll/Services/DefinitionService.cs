using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain;
using ActivitySeeker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivitySeeker.Bll.Services
{
    public class DefinitionService : IDefinitionService
    {
        private readonly ActivitySeekerContext _context;

        public DefinitionService(ActivitySeekerContext context)
        {
            _context = context;
        }

        public async Task CreateState(BotState state)
        {
            await _context.BotStates.AddAsync(state);
            await _context.SaveChangesAsync();
        }

        public async Task CreateTransition(BotTransition transition)
        {
            await _context.Transitions.AddAsync(transition);
            await _context.SaveChangesAsync();
        }
    }
}
