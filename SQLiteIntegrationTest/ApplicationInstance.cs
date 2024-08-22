using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SQLiteApplication.Entity;

namespace SQLiteIntegrationTest;

internal class ApplicationInstance<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            RemoveAllDbContextsFromServices(services);

            services.AddDbContext<ApplicationContext>(options =>
            {
                var projectAssemblyName = "SQLiteIntegrationTest";
                options.UseSqlite("Data Source=TestDb.db", x => x.MigrationsAssembly(projectAssemblyName));
            });

            services.AddDbContext<TestContext>();

            var buildedProvider = services.BuildServiceProvider();

            var context = buildedProvider.GetService<TestContext>();

            context!.Database.EnsureDeleted();

            context.Database.Migrate();

        });
    }

    private void RemoveAllDbContextsFromServices(IServiceCollection services)
    {
        // reverse operation of AddDbContext<XDbContext> which removes  DbContexts from services
        var descriptors = services.Where(d => d.ServiceType.BaseType == typeof(DbContextOptions)).ToList();
        descriptors.ForEach(d => services.Remove(d));

        var dbContextDescriptors = services.Where(d => d.ServiceType.BaseType == typeof(DbContext)).ToList();
        dbContextDescriptors.ForEach(d => services.Remove(d));
    }
}