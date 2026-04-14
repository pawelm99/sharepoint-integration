using MediatR;
using SharePoint.Integration.Core.Components.SharePoint.Models.Response;

namespace SharePoint.Integration.Core.Components.SharePoint.Models.Requests
{
    public class AssignPermissionsToFolderRequest(string userEmail, string folderName) : IRequest<AssignPermissionsToFolderResponse>
    {
        public string UserEmail { get; } = userEmail;
        public string FolderName { get; } = folderName;
    }
}
