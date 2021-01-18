using System.Threading.Tasks;
using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.People.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.WebUI.Controllers
{
    [Authorize]
    public class PeopleController : ApiControllerBase
    {
        private readonly ICurrentUserService _currentUserService;

        public PeopleController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<ActionResult<string>> GetRelationsByPeopleIds(int targetPersonId, int personId)
        {
            return await Mediator.Send(new GetRelationsByPeopleIdsQuery()
            {
                UserId = _currentUserService.UserId,
                TargetPersonId = targetPersonId,
                PersonId = personId
            });
        }
    }
}