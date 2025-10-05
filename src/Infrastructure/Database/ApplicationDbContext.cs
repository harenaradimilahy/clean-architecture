using Application.Data;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<User> Users => Set<User>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().ToTable("Users", Schemas.Default);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = new Guid("bb831866-fd4e-4307-ad1b-af07e3286d55"),
                Email = "admin@example.com",
                FirstName = "Admin",
                LastName = "User",
                PasswordHash = "AQAAAAEAACcQAAAAE..."
            },
            new User
            {
                Id = new Guid("d8381c6c-7360-4a5e-b52a-deec75d6a789"),
                Email = "user@example.com",
                FirstName = "John",
                LastName = "Doe",
                PasswordHash = "AQAAAAEAACcQAAAAE..."
            }
        );

    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }

}
