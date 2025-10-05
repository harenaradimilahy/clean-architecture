using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Database;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    private readonly IConfiguration _configuration;
    public ApplicationDbContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public ApplicationDbContext CreateDbContext(string[] args)
    {

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=db_client;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Database"));

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
