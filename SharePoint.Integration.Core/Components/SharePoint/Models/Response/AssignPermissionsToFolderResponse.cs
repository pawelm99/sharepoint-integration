namespace SharePoint.Integration.Core.Components.SharePoint.Models.Response
{
    public class AssignPermissionsToFolderResponse(bool success, string? message)
    {
        public bool Success { get; } = success;
        public string? Message { get; } = message;
    }
}
