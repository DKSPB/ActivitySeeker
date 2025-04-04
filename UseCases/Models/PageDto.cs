using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivitySeeker.Bll.Models
{
    public class PageDto<T>
    {
        public int Total { get; set; }

        public IEnumerable<T>? Data { get; set; }
    }
}