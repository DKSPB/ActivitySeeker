using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivitySeeker.Bll.Services
{
    internal class ActivityService
    {
        private readonly ActivitySeekerContext _context;

        public ActivityService(ActivitySeekerContext context)
        {
            _context = context;
        }

        public IEnumerable<Activity> GetActivities(ActivityRequest requestParams)
        {

            return null;
        }
    }
}
