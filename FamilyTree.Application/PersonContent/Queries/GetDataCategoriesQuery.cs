using FamilyTree.Application.PersonContent.ViewModels;
using MediatR;
using System.Collections.Generic;

namespace FamilyTree.Application.PersonContent.Queries
{
    public class GetDataCategoriesQuery : IRequest<List<DataCategoryDto>>
    {
        public string UserId { get; set; }

        public int PersonId { get; set; }
    }
}
