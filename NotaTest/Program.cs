using Microsoft.EntityFrameworkCore;
using NotaTest.Models;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<DBTESTContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context  = scope.ServiceProvider.GetRequiredService<DBTESTContext>();
    context.Database.Migrate();
}

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
    }
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Nota}/{action=Listar}/{id?}");

app.Run();
