using SharePoint.Integration.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Configuration.AddAzureKeyVaultConfiguration(builder.Configuration);
builder.Host.ConfigureServices((context, services) =>
{
    services.AddIntegrationServices(context.Configuration);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(cfg =>
    {
        cfg.OAuthClientId(app.Configuration["SharePointIntegration:ClientId"]);
        cfg.RoutePrefix = "swagger";
        cfg.SwaggerEndpoint("/swagger/v1/swagger.json", "SharePoint Integration");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();    
app.UseAuthorization();

app.MapControllers();

app.Run();
