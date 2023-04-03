using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RunGroopWebApp.Services;
using System.Configuration;
using System.Reflection;
using WorkshopGroup.Data;
using WorkshopGroup.Models;
using WorkshopGroup.Repository;
using WorkshopGroup.Repository.Interfaces;
using WorkshopGroup.Services;
using WorkshopGroup.Services.Extensions;
using WorkshopGroup.Services.Helpers;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.


builder.Services.AddControllersWithViews();



//builder.Services.AddTransient<Seed>();
//builder.Services.AddTransient<SeedAgain>();


builder.Services.AddScoped<IClubRepository, ClubRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();

builder.Services.AddScoped<ISkillRepository, SkillRepository>();


builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
var tokenURL = builder.Services.Configure<IPInfo>(builder.Configuration.GetSection("IPInfoSettings"));


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            sqlServerOptions => sqlServerOptions.EnableRetryOnFailure())
        .EnableSensitiveDataLogging();
});

/*builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
        .EnableSensitiveDataLogging()
        .EnableRetryOnFailure();
});
*/

/*builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddRoleManager<RoleManager<IdentityRole>>();*/

//This will register the Identity services, RoleManager, and EntityFrameworkStores with the correct configurations.
builder.Services.AddCustomIdentity();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddMemoryCache();
builder.Services.AddSession();


///
///
///

builder.Services.SeedRoles().GetAwaiter().GetResult();

///
///
///
//await builder.Services.SeedRoles();

/*builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging();
});
*/

// Register the Swagger generator
/*builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Title", Version = "v1" });
});*/

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Title", Version = "v1" });

    // Set the XML comments file path
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Enable Swagger middleware to serve generated Swagger as a JSON endpoint
app.UseSwagger();






//seed.SeedData(app);
//seedAgain.SeedData(app);


// if (args.Length == 1 && args[0].ToLower() == "seeddata")
//{
//await seed.SeedUsersAndRolesAsync(app);
//await seedAgain.SeedUsersAndRolesAsync(app);


//}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

/*app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});*/
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});



app.Run();
