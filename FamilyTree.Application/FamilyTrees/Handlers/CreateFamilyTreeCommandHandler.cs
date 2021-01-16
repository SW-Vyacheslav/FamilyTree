using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.FamilyTrees.Commands;
using FamilyTree.Domain.Entities.Tree;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FamilyTree.Application.FamilyTrees.Handlers
{
    public class CreateFamilyTreeCommandHandler : IRequestHandler<CreateFamilyTreeCommand, int>
    {
        private readonly IApplicationDbContext _dataContext;

        public CreateFamilyTreeCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _dataContext = applicationDbContext;
        }
        public async Task<int> Handle(CreateFamilyTreeCommand request, CancellationToken cancellationToken)
        {
            FamilyTreeEntity entity = new FamilyTreeEntity();
            entity.Name = request.Name;

            _dataContext.FamilyTrees.Add(entity);
            await _dataContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
