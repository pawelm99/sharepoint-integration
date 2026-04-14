using SharePoint.Integration.SharePoint.Models;

namespace SharePoint.Integration.SharePoint.Interfaces
{
    public interface ISharePoint
    {
        public OperationResult AssignPermissionsToFolder(string userEmail, string folder);
    }
}
