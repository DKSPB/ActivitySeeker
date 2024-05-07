using ActivitySeeker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivitySeeker.Bll.Models
{
    internal class ActivityRequest
    {
        public Guid? Category { get; set; }

        public DateTime? SearchFrom { get; set; }

        public DateTime? SearchTo { get; set;}
    }
}
