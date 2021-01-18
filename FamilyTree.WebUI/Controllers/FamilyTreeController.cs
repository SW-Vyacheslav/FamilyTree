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

        public IActionResult Index()
        {
            return View("StartTree");
        }

        public IActionResult StartTree()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateFamilyTreeCommand createFamilyTreeCommand)
        {
            return await Mediator.Send(createFamilyTreeCommand);
        }

        [HttpGet]
        public async Task<ActionResult<List<FamilyTreeEntityVm>>> GetAll()
        {
            return await Mediator.Send(new GetAllFamilyTreesQuery() { UserId = _currentUserService.UserId });
        }

        [HttpGet]
        public async Task<ActionResult<FamilyTreeVm>> GetFamilyTreeById(int id, int personId, int wifeId = 0)
        {
            return await Mediator.Send(new GetFamilyTreeByIdQuery()
            {
                UserId = _currentUserService.UserId,
                FamilyTreeId = id,
                PersonId = personId,
                WifeId = wifeId
            });
        }

        [HttpGet]
        public async Task<ActionResult<BloodTreeVm>> GetBloodTreeById(int id, int bloodMainId, int currentMainId, int wifeId = 0)
        {
            return await Mediator.Send(new GetBloodTreeByIdQuery() 
            {
                UserId = _currentUserService.UserId,
                FamilyTreeId = id,
                BloodMainId = bloodMainId,
                CurrentMainId = currentMainId,
                WifeId = wifeId
            });
        }        
    }
}