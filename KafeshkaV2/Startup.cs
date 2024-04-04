using KafeshkaV2.Areas.Identity;
using KafeshkaV2.BL.implementations;
using KafeshkaV2.BL.interfaces;
using KafeshkaV2.BL.validators.payment;
using KafeshkaV2.DAL.implementations;
using KafeshkaV2.DAL.interfaces;
using KafeshkaV2.DAL.Model;
using KafeshkaV2.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

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
            configuration.RootPath = "../KafeshkaV2App/dist"; // Specify the root path of your Angular app
        });

        services.AddDbContextPool<AppDbContext>(options =>
        {
            options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection")));
        });

        services.AddDbContextPool<KafeshkaUserDbContext>(options =>
        {
            options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection")));
        });

        services.AddDbContextPool<RestaurantDbContext>(options =>
        {
            options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection")));
        });


        services.AddScoped<IUserBL, UserBL>();
        services.AddScoped<IUserDal, UserDal>();
        services.AddScoped<IDishDal, DishDal>();
        services.AddScoped<IPaymentDetail, PaymentDetail>();
        services.AddScoped<IDishIngredientDal, DishIngredientDal>();
        services.AddScoped<IIngredientDal, IngredientDal>();
        services.AddScoped<UserManager<KafeshkaAppUser>>();
        services.AddSingleton<PaymentDetailValidator>();

        services.AddDefaultIdentity<KafeshkaAppUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<KafeshkaUserDbContext>();

        services.AddRazorPages();
        services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            }
        );

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "KafeshkaV2", Version = "v1" }); });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger((SwaggerOptions options) =>
            {
                options.RouteTemplate = "swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "KafeshkaV2 v1"); });
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHttpsRedirection();
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseSpaStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseCors(options => options.WithOrigins("http://localhost").AllowAnyMethod().AllowAnyHeader());

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
        });
    }
}