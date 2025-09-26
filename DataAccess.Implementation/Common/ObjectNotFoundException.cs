using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Common
{
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException(string entityName, object key)
        : base($"Объект '{entityName}' с идентификатором '{key}' не найден.") { }
    }
}
