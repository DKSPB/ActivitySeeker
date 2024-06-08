using ActivitySeeker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivitySeeker.Bll.Models
{
    public class ActivityRequest
    {
        public Guid? ActivityTypeId { get; set; }

        public string ActivityType { get; set; } = "Все активности";

        public DateTime SearchFrom { get; set; } = DateTime.Now;

        public DateTime SearchTo { get; set; } = DateTime.Now;
        
        public override string ToString()
        {
            StringBuilder stringBuilder = new ();

            stringBuilder.AppendLine($"\t\t\tВыбери тип и период проведения активностей:");
            
            stringBuilder.AppendLine("- Тип активности:");
            stringBuilder.AppendLine(string.Concat("\t\t\t",ActivityType));
            
            stringBuilder.AppendLine("- Дата и время проведения:");
            stringBuilder.AppendLine($"\t\t\tСобытия, проходящие\n\t\t\tс {SearchFrom.ToString("dd.MM.yyyy HH:mm")}\n\t\t\tпо {SearchTo.ToString("dd.MM.yyyy HH:mm")}");
            
            return stringBuilder.ToString();
        }
    }
}
