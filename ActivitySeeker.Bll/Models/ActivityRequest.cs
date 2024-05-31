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
        public Guid ActivityTypeId { get; set; } = Guid.Empty;

        public DateTime SearchFrom { get; set; } = DateTime.Now;

        public DateTime SearchTo { get; set; } = DateTime.Now;
    }
}
