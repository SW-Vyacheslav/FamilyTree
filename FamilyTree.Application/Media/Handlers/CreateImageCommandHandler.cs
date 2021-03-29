using FamilyTree.Application.Media.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.Media.Handlers
{
    public class CreateImageCommandHandler : IRequestHandler<CreateImageCommand, int>
    {
        //TODO
        public async Task<int> Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {
            return 1;
        }
    }
}
