namespace Common.Data.Contexts.Configurations;

using Common.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

public abstract class BaseEntityTypeConfiguration<TKey, TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IIdEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public virtual string? SchemaName
    {
        get;
    }

    public virtual string? TableName
    {
        get;
    }

    protected const string DefaultSchema = "dbo";

    protected const string EntityTypeNamePostFix = "Entity";

    public void Configure(
        EntityTypeBuilder<TEntity> builder)
    {
        ConfigureColumns(builder);
        ConfigureIndex(builder);
        ConfigureKey(builder);
        ConfigureTableName(builder);
    }

    public abstract void ConfigureColumns(
        EntityTypeBuilder<TEntity> builder);

    public virtual void ConfigureKey(
        EntityTypeBuilder<TEntity> builder)
        => builder.HasKey(entity => entity.Id);

    protected abstract void ConfigureIndex(
        EntityTypeBuilder<TEntity> builder);

    protected virtual string GetTableName()
    {
        if (!string.IsNullOrWhiteSpace(TableName))
        {
            return TableName;
        }

        Type entityType = typeof(TEntity);
        string defaultTableName = entityType.Name;

        return defaultTableName.Contains(
                EntityTypeNamePostFix,
                StringComparison.InvariantCultureIgnoreCase)
            ? defaultTableName.Replace(
                EntityTypeNamePostFix,
                string.Empty)
            : defaultTableName;
    }

    private void ConfigureTableName(
        EntityTypeBuilder<TEntity> builder)
        => builder.ToTable(
            GetTableName(),
            SchemaName ?? DefaultSchema);
}