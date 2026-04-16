using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
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
            services.AddIdentityWebApi(config);
            services.RegisterSharePoint();
            services.AddSwagger(config);

            return services;
        }


        private static IServiceCollection RegisterMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile<SharePointMappings>());
            return services;
        }


        private static IServiceCollection RegisterMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Handler>());
            return services;
        }


        private static IServiceCollection RegisterSharePoint(this IServiceCollection services)
        {
            services.AddScoped<ISharePoint, SharePoint.SharePoint>();
            return services;
        }


        private static IServiceCollection AddIdentityWebApi(this IServiceCollection services, IConfiguration config)
        {
            string clientId = config["SharePointIntegration:ClientId"]!;
            string audience = $"api://{clientId}";
            string tenantId = config["SharePointIntegration:TenantId"]!;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Audience = audience;
                    options.Authority = $"https://login.microsoftonline.com/{tenantId}";
                });
                //.AddMicrosoftIdentityWebApi(config, "SharePointIntegration", JwtBearerDefaults.AuthenticationScheme);

            return services;
        }


        private static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration config)
        {
            string clientId = config["SharePointIntegration:ClientId"]!;
            string audience = $"api://{clientId}";
            string scope = $"{audience}/SharePointIntegration";
            string tenantId = config["SharePointIntegration:TenantId"]!;

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "SharePointIntegration", Version = "v1" });

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri($"{config["SharePointIntegration:Instance"]}{tenantId}/oauth2/v2.0/authorize"),
                            TokenUrl = new Uri($"{config["SharePointIntegration:Instance"]}{tenantId}/oauth2/v2.0/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {scope, "SharePointIntegration" }
                            }
                        }
                    }
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    [new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                    }] = new[] { scope }
                });
            });
            return services;
        }
    }
}
