using System.Collections.Generic;
using System.Threading.Tasks;
using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.FamilyTrees.Commands;
using FamilyTree.Application.FamilyTrees.Queries;
using FamilyTree.Application.FamilyTrees.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.WebUI.Controllers
{
    [Authorize]
    public class FamilyTreeController : ApiControllerBase
    {
        private readonly ICurrentUserService _currentUserService;

        public FamilyTreeController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateFamilyTreeCommand createFamilyTreeCommand)
        {
            return await Mediator.Send(createFamilyTreeCommand);
        }

        [HttpGet]
        public async Task<ActionResult<List<FamilyTreeEntityVm>>> Get()
        {
            return await Mediator.Send(new GetFamilyTreesQuery() { UserId = _currentUserService.UserId });
        }
    }
}