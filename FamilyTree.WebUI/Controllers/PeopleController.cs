using System.Threading.Tasks;
using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.People.Commands;
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

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreatePersonCommand command)
        {
            command.UserId = _currentUserService.UserId;

            return await Mediator.Send(command);
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetRelationsByPeopleIds(int treeId, int targetPersonId, int personId)
        {
            return await Mediator.Send(new GetRelationsByPeopleIdsQuery()
            {
                UserId = _currentUserService.UserId,
                TargetPersonId = targetPersonId,
                PersonId = personId,
                FamilyTreeId = treeId
            });
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePersonAvatarImage(int id, UpdatePersonAvatarImageCommand command)
        {
            if (command.Id != id)
                return BadRequest();

            command.UserId = _currentUserService.UserId;

            await Mediator.Send(command);

            return NoContent();
        }
    }
}