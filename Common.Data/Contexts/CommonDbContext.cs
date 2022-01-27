namespace Common.Data.Contexts;

using Common.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

public class CommonDbContext : DbContext
{
    internal CommonDbContext()
    {
    }

    public CommonDbContext(
        DbContextOptions<CommonDbContext> options)
        : base(options)
    {
    }

    public DbSet<NodeEntity>? Nodes

    {
        get;
        set;
    }

    public DbSet<NodeTypeEntity>? NodeTypes
    {
        get;
        set;
    }

    public DbSet<TemplateEntity>? Templates
    {
        get;
        set;
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        Assembly configurationAssembly = GetType().Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(configurationAssembly);

        base.OnModelCreating(modelBuilder);
    }
}