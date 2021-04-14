using FamilyTree.Application.Common.Interfaces;
using FamilyTree.Application.Media.Commands;
using FamilyTree.Application.Media.Queries;
using FamilyTree.Application.Media.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyTree.WebUI.Controllers
{
    [Authorize]
    public class MediaController : ApiControllerBase
    {
        private readonly ICurrentUserService _currentUserService;

        public MediaController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ImageDto>>> GetImages(int dataBlockId)
        {
            return await Mediator.Send(new GetImagesQuery()
            {
                UserId = _currentUserService.UserId,
                DataBlockId = dataBlockId
            });
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateImage(CreateImageCommand command)
        {
            command.UserId = _currentUserService.UserId;

            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateImageDetails(int id, UpdateImageDetailsCommand command)
        {
            if (command.Id != id)
                return BadRequest();

            command.UserId = _currentUserService.UserId;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteImage(int id)
        {
            await Mediator.Send(new DeleteImageCommand() 
            {
                Id = id,
                UserId = _currentUserService.UserId
            });

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CopyImages(CopyImagesCommand command)
        {
            command.UserId = _currentUserService.UserId;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<VideoDto>>> GetVideos(int dataBlockId)
        {
            return await Mediator.Send(new GetVideosQuery()
            {
                UserId = _currentUserService.UserId,
                DataBlockId = dataBlockId
            });
        }

        [HttpGet]
        public async Task<FileResult> GetVideo(int id)
        {
            var fileVm = await Mediator.Send(new GetVideoQuery()
            {
                UserId = _currentUserService.UserId,
                Id = id
            });

            return File(fileVm.FileStream, $"video/{fileVm.FileFormat}", true);
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 524288000)]
        [RequestSizeLimit(524288000)]
        public async Task<ActionResult<int>> CreateVideo(CreateVideoCommand command)
        {
            command.UserId = _currentUserService.UserId;

            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateVideoDetails(int id, UpdateVideoDetailsCommand command)
        {
            if (command.Id != id)
                return BadRequest();

            command.UserId = _currentUserService.UserId;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteVideo(int id)
        {
            await Mediator.Send(new DeleteVideoCommand()
            {
                Id = id,
                UserId = _currentUserService.UserId
            });

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CopyVideos(CopyVideosCommand command)
        {
            command.UserId = _currentUserService.UserId;

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
