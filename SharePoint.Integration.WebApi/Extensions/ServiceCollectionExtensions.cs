using Microsoft.AspNetCore.Authentication.JwtBearer;
using SharePoint.Integration.Core.Components.SharePoint;
using SharePoint.Integration.Core.Components.SharePoint.Mappings;
using SharePoint.Integration.SharePoint.Interfaces;

namespace SharePoint.Integration.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIntegrationServices(this IServiceCollection services, IConfiguration config)
        {
            services.RegisterMapper();
            services.RegisterMediator();
            services.AddAuthenticationBearer(config);
            services.RegisterSharePoint();

            return services;
        }


        public static IServiceCollection RegisterMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile<SharePointMappings>());
            return services;
        }


        public static IServiceCollection RegisterMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Handler>());
            return services;
        }


        public static IServiceCollection RegisterSharePoint(this IServiceCollection services)
        {
            services.AddScoped<ISharePoint, SharePoint.SharePoint>();
            return services;
        }


        public static IServiceCollection AddAuthenticationBearer(this IServiceCollection services, IConfiguration config)
        {
            string audience = config["SharePointIntegration:Audience"]!;
            string tenantId = config["SharePointIntegration:TenantId"]!;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Audience = audience;
                    options.Authority = $"https://login.microsoftonline.com/{tenantId}";
                });

            return services;
        }
    }
}
