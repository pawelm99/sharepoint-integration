using MediatR;
using Microsoft.Extensions.Logging;
using SharePoint.Integration.Core.Components.SharePoint.Models.Requests;
using SharePoint.Integration.Core.Components.SharePoint.Models.Response;
using SharePoint.Integration.SharePoint.Interfaces;

namespace SharePoint.Integration.Core.Components.SharePoint
{
    public class Handler(ILogger<Handler> logger, ISharePoint sharePoint) 
        : IRequestHandler<AssignPermissionsToFolderRequest, AssignPermissionsToFolderResponse>
    {
        public async Task<AssignPermissionsToFolderResponse> Handle(AssignPermissionsToFolderRequest request, CancellationToken cancellationToken)
        {
            logger.LogTrace($"Handle request AssignPermissionsToFolderRequest.");
            var result = sharePoint.AssignPermissionsToFolder(request.UserEmail, request.FolderName);
            return new AssignPermissionsToFolderResponse(result.Success, result.Error);
        }
    }
}
