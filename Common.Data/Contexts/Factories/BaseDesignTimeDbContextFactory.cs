namespace Common.Data.Contexts.Factories;

using Common.Data.Contexts.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class BaseDesignTimeDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>
    where TContext : DbContext
{
    public TContext CreateDbContext(
        string[] args)
    {
        if (args is null
            || args.Length < 4)
        {
            throw new ApplicationException(
                $"Must define a Server, Database, UserId, and Password.");
        }

        string server = args[0];
        string database = args[1];
        string userId = args[2];
        string password = args[3];
        var connectionString = new SqlServerConnectionString(
            database,
            server,
            userId,
            password);
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<TContext>();
        dbContextOptionsBuilder.UseSqlServer(connectionString.GetConnectionString());

        try
        {
            var context = (TContext?)Activator.CreateInstance(
              typeof(TContext),
              dbContextOptionsBuilder.Options);

            return context is null
                ? throw new ApplicationException(
                    $"Failed to initialize a connection.")
                : context;
        }
        catch (Exception)
        {
            throw;
        }
    }
}