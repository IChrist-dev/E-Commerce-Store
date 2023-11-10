using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using INET_2005_Final_Project.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using INET_2005_Final_Project.Pages.Admin;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<INET_2005_Final_ProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("INET_2005_Final_ProjectContext") ?? throw new InvalidOperationException("Connection string 'INET_2005_Final_ProjectContext' not found.")));

// Configure authentication cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.LoginPath = "/Admin/Login";
        options.LogoutPath = "/Admin/Logout";
        options.AccessDeniedPath = "/Admin/AccessDenied";
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
