﻿using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.Privacy.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FamilyTree.WebUI.Controllers
{
    [Authorize]
    public class PrivacyController : ApiControllerBase
    {
        private readonly ICurrentUserService _currentUserService;

        public PrivacyController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDataHolderPrivacy(int id, UpdateDataHolderPrivacyCommand command)
        {
            if (command.Id != id)
                return BadRequest();

            command.UserId = _currentUserService.UserId;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateImagePrivacy(int id, UpdateImagePrivacyCommand command)
        {
            if (command.Id != id)
                return BadRequest();

            command.UserId = _currentUserService.UserId;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateVideoPrivacy(int id, UpdateVideoPrivacyCommand command)
        {
            if (command.Id != id)
                return BadRequest();

            command.UserId = _currentUserService.UserId;

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
