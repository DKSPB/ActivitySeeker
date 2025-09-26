using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.DI
{
    public class VkSecretKeyOption
    {
        public const string SectionName = "Vk";

        public string SecretKey { get; set; } = string.Empty;
    }
}
