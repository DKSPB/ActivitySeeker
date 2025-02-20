using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;

namespace ActivitySeeker.Api.TelegramBot
{
    public class StateManager
    {
        private readonly IDefinitionService _definitionService;
        private readonly IUserService _userService;
        public StateManager(IDefinitionService definitionService, IUserService userService)
        {
            _definitionService = definitionService;
            _userService = userService;
        }
        public async Task ToNextState(UserDto currentUser, string userCommand)
        {
            var transition = await _definitionService.GetTransitionByName(userCommand);

            currentUser.State.StateNumber_new = transition.ToStateId;

            await _userService.UpdateUser(currentUser);
        }
    }
}
