using System.Threading.Tasks;
using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.FamilyTies.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.WebUI.Controllers
{
    [Authorize]
    public class FamilyTiesController : ApiControllerBase
    {
        private readonly ICurrentUserService _currentUserService;

        public FamilyTiesController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetFamilyTiesByPersonId(int personId, int wifeId)
        {
            return await Mediator.Send(new GetFamilyTiesByPersonIdQuery()
            {
                UserId = _currentUserService.UserId,
                PersonId = personId,
                WifeId = wifeId
            });
        }
    }
}