using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Activity.Commands.Unpublish
{
    public record UnpublishActivityCommand(Guid ActivityId) : IRequest;
}
