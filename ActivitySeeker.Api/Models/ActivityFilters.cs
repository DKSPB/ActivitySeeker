using ActivitySeeker.Bll.Models;

namespace ActivitySeeker.Api.Models
{
    public class ActivityFilters
    {
        public int Limit { get; set; } = 20;

        public int Offset { get; set; } = 0;

        public ActivityRequest ActivityRequest { get; set; } = new ActivityRequest();
    }
}
