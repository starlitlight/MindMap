using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Versioning;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        AddControllers(services);
        AddApiVersioning(services);
        ConfigureRouting(services);
    }

    private static void AddControllers(IServiceCollection services)
    {
        services.AddControllers();
    }

    private static void AddApiVersioning(IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("X-Version"),
                new MediaTypeApiVersionReader("version"));
        });
    }

    private static void ConfigureRouting(IServiceCollection services)
    {
        services.AddRouting(options => options.LowercaseUrls = true);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        ConfigureEnvironment(app, env);
        ConfigureHttpsRedirection(app);
        UseStaticFiles(app);
        UseRouting(app);
        UseAuthorization(app);
        ConfigureEndpoints(app);
    }

    private static void ConfigureEnvironment(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
    }

    private static void ConfigureHttpsRedirection(IApplicationBuilder app)
    {
        app.UseHttpsRedirection();
    }

    private static void UseStaticFiles(IApplicationBuilder app)
    {
        app.UseStaticFiles();
    }

    private static void UseRouting(IApplicationBuilder app)
    {
        app.UseRouting();
    }

    private static void UseAuthorization(IApplicationBuilder app)
    {
        app.UseAuthorization();
    }

    private static void ConfigureEndpoints(IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}