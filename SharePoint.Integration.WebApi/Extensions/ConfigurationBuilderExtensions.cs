using Azure.Identity;

namespace SharePoint.Integration.WebApi.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static void AddAzureKeyVaultConfiguration(this IConfigurationManager configuration, IConfiguration config)
        {
            configuration.AddAzureKeyVault(
                vaultUri: new Uri($"https://{config["KeyVault"]}.vault.azure.net/"),
                credential: new DefaultAzureCredential(new DefaultAzureCredentialOptions
                {
                    ExcludeEnvironmentCredential = true,
                    ExcludeInteractiveBrowserCredential = true,
                    ExcludeVisualStudioCodeCredential = true,
                    ExcludeVisualStudioCredential = true,
                    ExcludeAzureCliCredential = false,
                    ExcludeManagedIdentityCredential = false,
                }));
        }
    }
}
