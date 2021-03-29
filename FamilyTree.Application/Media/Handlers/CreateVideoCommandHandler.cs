using FamilyTree.Application.Media.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.Media.Handlers
{
    public class CreateVideoCommandHandler : IRequestHandler<CreateVideoCommand, int>
    {
        public async Task<int> Handle(CreateVideoCommand request, CancellationToken cancellationToken)
        {
            return 0;
        }
    }
}
