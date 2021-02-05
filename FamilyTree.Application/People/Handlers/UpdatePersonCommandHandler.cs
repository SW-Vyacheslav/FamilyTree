using FamilyTree.Application.People.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.People.Handlers
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand>
    {
        public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
