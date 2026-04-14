using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharePoint.Integration.Core.Components.SharePoint.Models;
using SharePoint.Integration.Core.Components.SharePoint.Models.Requests;

namespace SharePoint.Integration.WebApi.Controllers.Components.SharePoint
{
    [Authorize]
    [ApiController]
    [Route("api/sharePoint")]
    public class SharePointController(IMapper mapper, IMediator mediator) : ControllerBase
    {
        [HttpPost("assignPermissionsToFolder")]
        public async Task<IActionResult> Post(AssignPermissionsToFolderModel payload, CancellationToken cancellationToken = default)
            => Ok(mediator.Send(mapper.Map<AssignPermissionsToFolderRequest>(payload), cancellationToken));
    }
}
