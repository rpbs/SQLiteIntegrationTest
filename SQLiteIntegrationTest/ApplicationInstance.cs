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
            // I had to remove all the contexts from services 
            // This was a mandatory thing to achieve the goal
            var contextServices = services
            .Where(x => x.ServiceType.BaseType == typeof(DbContextOptions) || x.ServiceType.BaseType == typeof(DbContext)).ToList();

            foreach (var context in contextServices)
            {
                services.Remove(context);
            }

            // Adding the main Context from the application but chaning the connection to use SQLite.
            services.AddDbContext<ApplicationContext>(options =>
            {
                var projectAssemblyName = "SQLiteIntegrationTest";
                options.UseSqlite("Data Source=TestDb.db", x => x.MigrationsAssembly(projectAssemblyName));
            });

            // Adding the TestContext
            services.AddDbContext<TestContext>();

            // Building services  
            var buildedProvider = services.BuildServiceProvider();
            // Getting our Context that is responsible for Creating our SQLite database file. 
            var testContext = buildedProvider.GetService<TestContext>();
            // This deletes the SQLite file so each new test will generate a new database.
            testContext!.Database.EnsureDeleted();
            // Executes the migration so that creates the database and tables
            testContext.Database.Migrate();

        });
    }
}