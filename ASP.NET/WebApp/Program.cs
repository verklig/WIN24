using Data.Contexts;
using Data.Entities;
using Data.Repositories;
using Infrastructure.Services;
using Infrastructure.Interfaces;
using Infrastructure.Seeders;
using WebApp.Hubs;
using WebApp.SignalR;
using WebApp.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews(options =>
{
  options.Filters.Add<NotificationsFilter>();
});
builder.Services.AddSignalR();
builder.WebHost.UseUrls("http://0.0.0.0:7777");

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(x => x.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddIdentity<UserEntity, IdentityRole>(options =>
{
  options.User.RequireUniqueEmail = true;
  options.Password.RequiredLength = 8;
  options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
}).AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
  options.Password.RequireNonAlphanumeric = false;
  options.Password.RequireDigit = true;
  options.Password.RequireUppercase = true;
  options.Password.RequireLowercase = true;
  options.Password.RequiredLength = 8;
});
builder.Services.ConfigureApplicationCookie(options =>
{
  options.LoginPath = "/auth/login";
  options.AccessDeniedPath = "/auth/denied";
  options.Cookie.HttpOnly = true;
  options.Cookie.IsEssential = true;
  options.ExpireTimeSpan = TimeSpan.FromHours(1);
  options.SlidingExpiration = true;
});

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<INotificationPusher, NotificationPusher>();

builder.Services.AddScoped<NotificationsFilter>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  await AdminSeeder.SeedAdminUser(services);
}
app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<NotificationHub>("/notificationHub");
app.Run();
