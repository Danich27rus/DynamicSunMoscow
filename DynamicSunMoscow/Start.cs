using Infrastructure.Base;

namespace DynamicSunMoscow;

public class Start
{
    public IConfiguration _config { get; }
    public Start(IConfiguration configuration)
    {
        _config = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        //services.AddApplicationLayer();
        //services.AddIdentityInfrastructure(_config);
        //services.AddBaseInfrastructure(_config);
        //services.AddSharedInfrastructure(_config);
        //services.AddSwaggerExtension();
        //services.AddApiVersioningExtension();
        services.AddControllersWithViews();
        services.AddHealthChecks();
        //services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHealthChecks("/health");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
