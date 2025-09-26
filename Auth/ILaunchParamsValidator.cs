using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public interface ILaunchParamsValidator
    {
        /// <summary>
        /// Валидация параметров запуска vk_mini_apps
        /// </summary>
        /// <param name="parameters">Набор параметров, передаваемых клиентской стороной</param>
        /// <returns>Результат валидации</returns>
        bool Validate(IDictionary<string, string> parameters);
    }
}
