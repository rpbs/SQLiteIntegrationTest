using Microsoft.EntityFrameworkCore;
using SQLiteApplication.Entity;

namespace SQLiteIntegrationTest;

public class TestContext : ApplicationContext
{
    public TestContext()
    {
    }

    public TestContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=TestDb.db");        
    }
}
