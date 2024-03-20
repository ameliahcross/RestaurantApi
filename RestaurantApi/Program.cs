using RestaurantApi.Infrastructure.Persistence;
using RestaurantApi.Core.Application;
using RestaurantApi.Infrastructure.Shared;
using RestaurantApi.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using RestaurantApi.Infrastructure.Identity.Entities;
using RestaurantApi.Infrastructure.Identity.Seeds;
using WebApp.RestaurantApi.Middlewares;

//inicia la configuración del host de la aplicación. Esto realiza la función de un método Main
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSession();
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddIdentityInfrasastructure(builder.Configuration);
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddLogging();
builder.Services.AddScoped<LoginAuthorize>();
builder.Services.AddTransient<ValidateUserSession, ValidateUserSession>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedAsync(userManager, roleManager);
        await DefaultSuperAdminUser.SeedAsync(userManager, roleManager);
        await DefaultAdminUser.SeedAsync(userManager, roleManager);
        await DefaultMeseroUser.SeedAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error seeding data: {ex.Message}");
        Console.WriteLine(ex.StackTrace);
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.Use, configuran el middleware que la aplicación usará para manejar las solicitudes HTTP
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// hay dos metodos middlewares, user autentication y user authorization. Siempre user autentication va primero(arriba)
// autentication = verificar si el usuario existe. authorization = autorizo a entrar a donde tenga acceso
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
