using RestApi.Template.Api;
using RestApi.Template.Application;
using RestApi.Template.Infra;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Presentation.Middleware;
using Prometheus;
using Serilog;
using RestApi.Common;

#pragma warning disable S1199

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddCommon(builder.Configuration)
        .AddPresentation(
            builder.Configuration,
            builder.Environment.IsProduction()
        )
        .AddInfrastructure(builder.Configuration)
        .AddApplication();

    builder.Host.UseSerilog((context, config) =>
        config.ReadFrom.Configuration(context.Configuration)
    );
}

WebApplication app = builder.Build();
{
    app.UseResponseCompression();

    if (!app.Environment.IsProduction())
        app
            .UseSwagger(o => o.RouteTemplate = "api/{documentName}/swagger.{json|yaml}")
            .UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/api/v1/swagger.json", "API Template v1");
                o.InjectStylesheet("/swagger.css");
                o.RoutePrefix = "docs";
            })
            .UseDeveloperExceptionPage();

    app
        .UseRouting()
        .UseStaticFiles()
        .UseMiddleware<ExceptionHandlingMiddleware>()
        .UseMiddleware<AuthMiddleware>();

    app
        .UseHealthChecks("/_health", new HealthCheckOptions()
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        })
        .UseEndpoints(endpoints =>
        {
            if (!app.Environment.IsProduction())
                endpoints.MapHealthChecksUI(o =>
                {
                    o.UIPath = "/dashboard";
                    o.PageTitle = "Health | RestApi-Api-Template";
                    //o.AddCustomStylesheet("wwwroot/health.css");
                });

            endpoints.MapMetrics("/_metrics");

            endpoints.MapControllers();
        });

    app.Run();
}