using System.Text;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using Auth.DI;

namespace Auth
{
    public class VkLaunchParamsValidator : ILaunchParamsValidator
    {
        private readonly string _secretKey;

        public VkLaunchParamsValidator(IOptions<VkSecretKeyOption> secretKeyOption)
        {
            _secretKey = secretKeyOption.Value.SecretKey;
        }
        public bool Validate(IDictionary<string, string> parameters)
        {
            if (!parameters.TryGetValue("sign", out var sign))
            {
                return false;
            }
                
            var data = parameters
                .Where(p => p.Key.StartsWith("vk_"))
                .OrderBy(p => p.Key)
                .Select(p => $"{p.Key}={p.Value}");

            var queryString = string.Join("&", data);

            var hash = Convert.ToBase64String(
                HMACSHA256.HashData(
                    Encoding.UTF8.GetBytes(_secretKey),
                    Encoding.UTF8.GetBytes(queryString)
                )
            ).TrimEnd('=').Replace('+', '-').Replace('/', '_');

            return hash == sign;
        }
    }
}