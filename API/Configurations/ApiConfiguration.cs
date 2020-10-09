using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;

namespace API.Configurations
{
    public static class ApiConfiguration
    {

        public static IServiceCollection AddApiVersioningSupport(this IServiceCollection services)
        {
            services
                .AddApiVersioning(options => options.ReportApiVersions = true)
                .AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVVV");

            return services;
        }

        public static IApplicationBuilder UseCustomPrefix(this IApplicationBuilder app, string prefix)
        {
            app.UsePathBase($"/{prefix}");

            app.Use((context, next) =>
            {
                context.Request.PathBase = $"/{prefix}";
                return next();
            });

            return app;
        }

        public static SwaggerGenOptions AddSwaggerGenSupport(
            this SwaggerGenOptions swaggerGenOptions, IHostEnvironment hostEnvironment, string title, string release, List<string> apiVersions = null)
        {
            if (apiVersions is null || apiVersions.Count == 0)
                apiVersions = new List<string> { "1.0" };

            string description = $"Environment: {hostEnvironment.EnvironmentName} {Environment.NewLine}Release: {release}";

            apiVersions.ForEach(version =>
            {
                swaggerGenOptions.SwaggerDoc($"v{version}", new OpenApiInfo
                {
                    Title = title,
                    Version = $"v{version}",
                    Description = description,
                    TermsOfService = new Uri("https://www.google.com"),
                    Contact = new OpenApiContact
                    {
                        Email = "test@gmail.com",
                        Name = "Test Andrei"
                    },
                    License = new OpenApiLicense
                    {
                        Name = $"Copyright {DateTime.Now.Year}, GiftClub All rights reserved."
                    }

                });
            });

            swaggerGenOptions.EnableAnnotations();

            return swaggerGenOptions;
                
        }
    }
}