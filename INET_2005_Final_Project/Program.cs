using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using INET_2005_Final_Project.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<INET_2005_Final_ProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("INET_2005_Final_ProjectContext") ?? throw new InvalidOperationException("Connection string 'INET_2005_Final_ProjectContext' not found.")));

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
