using Microsoft.EntityFrameworkCore;
using WebIdentityAuthorization.Services;
using Microsoft.AspNetCore.Identity;
using WebIdentityAuthorization.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//add connection string to the service container to be requested by any other class
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    //create a variable and call it connectionString
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    //configure the ApplicationDbContext to use SQL server
    options.UseSqlServer(connectionString);
});

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


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
