using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.PersonContent.Commands;
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

        [HttpGet]
        public async Task<ActionResult<List<DataCategoryDto>>> GetDataCategories(int personId)
        {
            return await Mediator.Send(new GetDataCategoriesQuery() 
            {
                PersonId = personId,
                UserId = _currentUserService.UserId
            });
        }

        [HttpGet]
        public async Task<ActionResult<DataCategoryVm>> GetDataCategory(int dataCategoryId)
        {
            return await Mediator.Send(new GetDataCategoryQuery()
            {
                DataCategoryId = dataCategoryId,
                UserId = _currentUserService.UserId
            });
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateDataCategory(CreateDataCategoryCommand command)
        {
            command.UserId = _currentUserService.UserId;

            return await Mediator.Send(command);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateDataBlock(CreateDataBlockCommand command)
        {
            command.UserId = _currentUserService.UserId;

            return await Mediator.Send(command);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateDataHolder(CreateDataHolderCommand command)
        {
            command.UserId = _currentUserService.UserId;

            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDataCategoryName(int id, UpdateDataCategoryNameCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            command.UserId = _currentUserService.UserId;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDataBlockTitle(int id, UpdateDataBlockTitleCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            command.UserId = _currentUserService.UserId;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDataHolderData(int id, UpdateDataHolderDataCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            command.UserId = _currentUserService.UserId;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDataHolderTitle(int id, UpdateDataHolderTitleCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            command.UserId = _currentUserService.UserId;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDataCategoryOrder(int id, UpdateDataCategoryOrderCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            command.UserId = _currentUserService.UserId;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDataBlockOrder(int id, UpdateDataBlockOrderCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            command.UserId = _currentUserService.UserId;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDataHolderOrder(int id, UpdateDataHolderOrderCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            command.UserId = _currentUserService.UserId;

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
