using Microsoft.EntityFrameworkCore;
using WksJobsMvc.Data;
using Microsoft.AspNetCore.Identity;
using WksJobsMvc.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<WksDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("WksDbContextConnection")));

builder.Services.AddDefaultIdentity<DefaultUser>().AddEntityFrameworkStores<WksDbContext>();

//builder.Services.AddDefaultIdentity<WksJobsMvcUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<WksJobsMvcContext>();

var app = builder.Build();

CreateDbIfNotExists(app);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//    endpoints.MapRazorPages();
//});

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();
//app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();


app.Run();


static void CreateDbIfNotExists(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<WksDbContext>();
            DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}
