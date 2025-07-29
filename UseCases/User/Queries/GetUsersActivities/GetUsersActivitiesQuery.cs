using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Activity.Models;
using UseCases.Common;

namespace UseCases.User.Queries.GetUsersActivities
{
    public record GetUsersActivitiesQuery(long UserId, int Limit = 20, int Offset = 0) : IRequest<PagedResult<ActivityDto>>;
}
