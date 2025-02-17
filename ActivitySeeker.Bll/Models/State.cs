﻿using ActivitySeeker.Domain.Entities;
using System.Text;

namespace ActivitySeeker.Bll.Models
{
    public class State
    {
        public bool? ActivityFormat { get; set; }

        public ActivityTypeDto ActivityType { get; set; } = new("Все виды активности");

        public DateTime? SearchFrom { get; set; }

        public DateTime? SearchTo { get; set; }
        
        public int MessageId { get; set; }
        
        public StatesEnum StateNumber { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new ();

            stringBuilder.AppendLine($"\t\t\tВыбери тип и период проведения активностей:");
            
            stringBuilder.AppendLine("- Тип активности:");
            stringBuilder.AppendLine(string.Concat("\t\t\t", ActivityType.TypeName));

            stringBuilder.AppendLine("- Формат проведения:");

            if (ActivityFormat is null)
            {
                stringBuilder.AppendLine("\t\t\tЛюбой");
            }
            else if (ActivityFormat.Value)
            {
                stringBuilder.AppendLine("\t\t\tОнлайн");
            }
            else 
            {
                stringBuilder.AppendLine("\t\t\tОфлайн");
            }
            
            stringBuilder.AppendLine("- Дата и время проведения:");
            stringBuilder.AppendLine($"\t\t\tСобытия, проходящие\n\t\t\tс " +
                                     $"{SearchFrom.GetValueOrDefault().ToString("dd.MM.yyyy HH:mm")}" +
                                     $"\n\t\t\tдо {SearchTo.GetValueOrDefault().ToString("dd.MM.yyyy HH:mm")}");
            
            return stringBuilder.ToString();
        }
    }
}
