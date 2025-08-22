using Auth;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;

namespace ActivitySeeker.Api.Auth
{
    public class VkAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ILaunchParamsValidator _launchParamsValidator;

        public VkAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock, 
        ILaunchParamsValidator launchParamsValidator) : base(options, logger, encoder, clock)
        {
            _launchParamsValidator = launchParamsValidator;
        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var query = Context.Request.Query
                .ToDictionary(x => x.Key, x => x.Value.ToString());

            if (!_launchParamsValidator.Validate(query))
            {
                return Task.FromResult(AuthenticateResult.Fail("Неверная подпись вк"));
            }

            if (!long.TryParse(query["vk_user_id"], out var userId))
            {
                return Task.FromResult(AuthenticateResult.Fail("Некорректный идентификатор пользователя"));
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
