using DataAccess.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public sealed class ActivityAuthorization : BaseAuthorization<Activity, Guid>
    {
        public ActivityAuthorization(IDbContext context) : base(context)
        { }

        protected override DbSet<Activity> Set => _context.Activities;

        protected override Expression<Func<Activity, bool>> IsOwnerPredicate(Guid id, long userId)
        {
            return x => x.UserId == userId || x.Id == id;
        }
    }
}
