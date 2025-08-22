namespace ActivitySeeker.Api.Extensions;

public static class SwaggerUiConfiguration
{
    public static IApplicationBuilder UseSwaggerUiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            return app;
        }
        
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Activity Seeker API v1");
            options.DocumentTitle = "Activity Seeker API Docs";
            options.RoutePrefix = "swagger";
            options.ConfigObject.AdditionalItems["tryItOutEnabled"] = true;
        });

        return app;
    }
}