using Admin.EndPoint.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mvsBuilder = builder.Services.AddControllersWithViews();
mvsBuilder.AddRazorRuntimeCompilation();

builder.Services.AddSignalR();

builder.Services.AddAuthentication(option => 
{
    option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(Options =>
{
    Options.LoginPath = "/Account/Login";
    Options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    Options.AccessDeniedPath = "/Account/AccessDenied";
});

var app = builder.Build();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatRoomHub>("/chatHub");

app.Run();
