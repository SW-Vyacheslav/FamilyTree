using FamilyTree.Application.PersonContent.ViewModels;
using MediatR;

namespace FamilyTree.Application.PersonContent.Queries
{
    public class GetDataCategoryQuery : IRequest<DataCategoryVm>
    {
        public string UserId { get; set; }

        public int DataCategoryId { get; set; }
    }
}
