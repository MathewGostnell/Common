namespace Common.Data.Contexts.Factories;

using Microsoft.EntityFrameworkCore;
using System.Configuration;

public abstract class BaseDbContextFactory<TContext> : IDbContextFactory<TContext>
    where TContext : DbContext
{
    public virtual string ConnectionStringName => GetType().Name;

    public TContext CreateDbContext()
    {
        string connectionString = ConfigurationManager
            .ConnectionStrings[ConnectionStringName]
            .ConnectionString;
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ConfigurationErrorsException(
                $"Failed to find a value for {ConnectionStringName} configuration.");
        }

        var dbContextOptionsBuilder = new DbContextOptionsBuilder<TContext>();
        dbContextOptionsBuilder.UseSqlServer(connectionString);

        TContext? context = TryCreateDbContext(dbContextOptionsBuilder);
        return context is null
            ? throw new ApplicationException()
            : context;
    }

    protected TContext? TryCreateDbContext(
        DbContextOptionsBuilder<TContext> dbContextOptionsBuilder)
    {
        try
        {
            Type? contextType = typeof(TContext);
            return (TContext?)Activator.CreateInstance(
                contextType,
                dbContextOptionsBuilder.Options);
        }
        catch (Exception)
        {
            return null;
        }
    }
}