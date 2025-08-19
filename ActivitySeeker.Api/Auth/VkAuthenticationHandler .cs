using Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

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
            var query = Context.Request.Query.ToDictionary(x => x.Key, x => x.Value.ToString());

            if (!_launchParamsValidator.Validate(query))
                return Task.FromResult(AuthenticateResult.Fail("Неверная подпись вк"));

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, query["vk_user_id"]),
                new Claim(ClaimTypes.AuthenticationMethod, "VK")
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
