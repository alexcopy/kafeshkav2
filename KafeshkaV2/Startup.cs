using System.Configuration;
using KafeshkaV2.BL.implementations;
using KafeshkaV2.BL.interfaces;
using KafeshkaV2.DAL.implementations;
using KafeshkaV2.DAL.interfaces;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.WebAssembly;
using Microsoft.Extensions.FileProviders;


public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        // Add other services here as needed

        // Register Spa static files
        services.AddSpaStaticFiles(configuration =>
        {
            configuration.RootPath = "../ClientApp/dist/client-app"; // Specify the root path of your Angular app
        });

        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection"))));

        services.AddScoped<IUserBL, UserBL>();
        services.AddScoped<IUserDal, UserDal>();
        services.AddScoped<IDishDal, DishDal>();
        services.AddScoped<IDishIngredientDal, DishIngredientDal>();
        services.AddScoped<IIngredientDal, IngredientDal>();

        services.AddControllersWithViews();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../ClientApp";
                spa.UseAngularCliServer(npmScript: "start");
            });
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(env.ContentRootPath, "../ClientApp/dist/client-app")),
                RequestPath = "/client-app"

        });


        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }


    // public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    // {
    //     if (env.IsDevelopment())
    //     {
    //         app.UseDeveloperExceptionPage();
    //     }
    //     else
    //     {
    //         app.UseExceptionHandler("/Home/Error");
    //         app.UseHsts();
    //     }
    //
    //     app.UseHttpsRedirection();
    //     app.UseEndpoints(endpoints =>
    //     {
    //         endpoints.MapControllerRoute(
    //             name: "default",
    //             pattern: "{controller=Home}/{action=Index}/{id?}");
    //     });
    //
    //     app.UseStaticFiles(new StaticFileOptions
    //     {
    //         FileProvider = new PhysicalFileProvider(
    //             Path.Combine(env.ContentRootPath, "ClientApp", "dist", "client-app")),
    //         RequestPath = "/client-app"
    //     });
    //     app.UseRouting();
    //
    //     app.UseAuthorization();
    //
    //     // Configure Angular SPA
    //     app.UseSpa(spa =>
    //     {
    //         spa.Options.SourcePath = "ClientApp";
    //
    //         if (env.IsDevelopment())
    //         {
    //             spa.UseAngularCliServer(npmScript: "start");
    //         }
    //     });
    // }
}