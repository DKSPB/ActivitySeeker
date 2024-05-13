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
        public List<Guid> ActvityTypeIds { get; set; } = default!;

        public DateTime SearchFrom { get; set; } = DateTime.Now;

        public DateTime? SearchTo { get; set; } = DateTime.Now;
    }
}
