using AppointmentManagementSystem.Application;
using AppointmentManagementSystem.Infrastructure;
using AppointmentManagementSystem.Infrastructure.Context;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});
builder.Services.AddAutoMapper(typeof(Program).Assembly,
    typeof(AppointmentManagementSystem.Application.Common.Mappings.MappingProfile).Assembly);
var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

   

        // Sýrayla seed iþlemlerini yap
        await ApplicationDbContextSeed.SeedDefaultRolesAsync(context);
        await ApplicationDbContextSeed.SeedDefaultUsersAsync(context);
        await ApplicationDbContextSeed.SeedDefaultServicesAsync(context);

        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Seed data baþarýyla eklendi.");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Seed data eklenirken bir hata oluþtu.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowAll");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
