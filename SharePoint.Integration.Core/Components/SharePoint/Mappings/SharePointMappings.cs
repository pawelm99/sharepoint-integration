using SharePoint.Integration.Core.Components.SharePoint.Models;
using SharePoint.Integration.Core.Components.SharePoint.Models.Requests;

namespace SharePoint.Integration.Core.Components.SharePoint.Mappings
{
    public class SharePointMappings : AutoMapper.Profile
    {
        public SharePointMappings()
        {
            CreateMap<AssignPermissionsToFolderModel, AssignPermissionsToFolderRequest>();
        }
    }
}