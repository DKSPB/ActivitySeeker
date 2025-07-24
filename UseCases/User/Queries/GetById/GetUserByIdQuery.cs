using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.User.Queries.GetById
{
    public record GetUserByIdQuery(long UserId) : IRequest;
}
