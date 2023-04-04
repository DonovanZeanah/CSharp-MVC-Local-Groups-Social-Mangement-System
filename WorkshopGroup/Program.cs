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
using WorkshopGroup.Services.interfaces;

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


builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddRoleManager<RoleManager<IdentityRole>>();

//This will register the Identity services, RoleManager, and EntityFrameworkStores with the correct configurations.
//builder.Services.AddCustomIdentity();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();


/*builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.User.RequireUniqueEmail = true;
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(UserRoles.Admin, policy => policy.RequireRole(UserRoles.Admin));
    options.AddPolicy(UserRoles.Leader, policy => policy.RequireRole(UserRoles.Leader));
    options.AddPolicy(UserRoles.Moderator, policy => policy.RequireRole(UserRoles.Moderator));
    options.AddPolicy(UserRoles.Guru, policy => policy.RequireRole(UserRoles.Guru));
    options.AddPolicy(UserRoles.User, policy => policy.RequireRole(UserRoles.User));
});*/


//builder.Services.AddScoped<UserManager<AppUser>>();
/*builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddUserManager<UserManager<AppUser>>();

builder.Services.AddIdentityCore<AppUser>()
    .AddUserManager<UserManager<AppUser>>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();*/

    //builder.Services.AddScoped<IRegisterUserService, RegisterUserService>();

//builder.Services.AddScoped<IRegisterUserService, RegisterUserService>();

/*builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();*/

builder.Services.AddMemoryCache();
builder.Services.AddSession();




/*builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging();
});
*/

///
///
///

//builder.Services.SeedRoles();

///
///
///
//await builder.Services.SeedUsers();
//await builder.Services.SeedRoles();

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
