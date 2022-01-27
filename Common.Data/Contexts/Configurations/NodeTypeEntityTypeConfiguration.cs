namespace Common.Data.Contexts.Configurations;

using Common.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class NodeTypeEntityTypeConfiguration : BaseEntityTypeConfiguration<int, NodeTypeEntity>
{
    public override void ConfigureColumns(
        EntityTypeBuilder<NodeTypeEntity> builder)
    {
        builder
            .Property(nodeType => nodeType.Name)
            .HasMaxLength(32)
            .IsUnicode(false);
        builder
            .Property(nodeType => nodeType.RegularExpression)
            .HasMaxLength(32)
            .IsUnicode(true);
    }

    protected override void ConfigureIndex(
        EntityTypeBuilder<NodeTypeEntity> builder)
    {
        builder
            .HasIndex(nodeType => nodeType.Name)
            .IsUnique();
        builder
            .HasIndex(nodeType => nodeType.RegularExpression)
            .IsUnique();
    }
}