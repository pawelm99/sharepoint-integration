using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core;
using SharePoint.Integration.SharePoint.Interfaces;
using SharePoint.Integration.SharePoint.Models;
using System.Web;

namespace SharePoint.Integration.SharePoint
{
    public class SharePoint(IConfiguration configuration, ILogger<SharePoint> logger) : ISharePoint
    {
        public OperationResult AssignPermissionsToFolder(string userEmail, string folderName)
        {
            try
            {
                logger.LogTrace($"Execute request AssignPermissionsToFolder with params userEmail: {userEmail} and folderName: {folderName}.");

                string clientId = configuration["SharePoint:ClientId"]!;
                string secret = configuration["SharePoint:Secret"]!;
                string siteUrl = configuration["SharePoint:Url"]!;
                string sharePointLibrary = HttpUtility.UrlPathEncode(configuration["SharePointLibrary"])!;

                RoleType role = RoleType.Editor;

                using var context = new AuthenticationManager().GetAppOnlyAuthenticatedContext(siteUrl, clientId, secret);
                Folder folder = context.Web.GetFolderByServerRelativeUrl($"{siteUrl}/{sharePointLibrary}/{folderName}");

                context.Load(folder);
                context.Load(folder.Files);
                context.ExecuteQuery();

                logger.LogTrace($"Clear permissions for folder");
                folder.ListItemAllFields.BreakRoleInheritance(false, true);
                context.ExecuteQuery();

                logger.LogTrace($"Set permissions for user: {userEmail}");

                User user = context.Web.EnsureUser(userEmail);
                context.Load(user);
                context.ExecuteQuery();

                RoleDefinitionBindingCollection roleBindings = new RoleDefinitionBindingCollection(context);
                RoleDefinition roleDefinition = context.Web.RoleDefinitions.GetByType(role);
                roleBindings.Add(roleDefinition);

                folder.ListItemAllFields.RoleAssignments.Add(user, roleBindings);
                context.ExecuteQuery();

                logger.LogTrace($"Finish!");
                return new OperationResult(true);
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message);
            }
        }
    }
}
