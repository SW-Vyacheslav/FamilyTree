using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.PersonContent.Queries;
using FamilyTree.Application.PersonContent.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyTree.WebUI.Controllers
{
    [Authorize]
    public class PersonContentController : ApiControllerBase
    {
        private readonly ICurrentUserService _currentUserService;

        public PersonContentController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<ActionResult<List<DataCategoryDto>>> GetDataCategories(int personId)
        {
            return await Mediator.Send(new GetDataCategoriesQuery() 
            {
                PersonId = personId,
                UserId = _currentUserService.UserId
            });
        }

        public async Task<ActionResult<DataCategoryVm>> GetDataCategory(int dataCategoryId)
        {
            return await Mediator.Send(new GetDataCategoryQuery()
            {
                DataCategoryId = dataCategoryId,
                UserId = _currentUserService.UserId
            });
        }
    }
}
