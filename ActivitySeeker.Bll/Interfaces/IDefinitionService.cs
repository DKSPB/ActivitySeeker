using ActivitySeeker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivitySeeker.Bll.Interfaces
{
    public interface IDefinitionService
    {
        Task CreateState(BotState state);

        Task CreateTransition(BotTransition transition);
    }
}
