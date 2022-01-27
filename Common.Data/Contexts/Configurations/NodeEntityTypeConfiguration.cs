namespace Common.Data.Contexts.Configurations;

using Common.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class NodeEntityTypeConfiguration : BaseEntityTypeConfiguration<int, NodeEntity>
{
    public override void ConfigureColumns(
        EntityTypeBuilder<NodeEntity> builder)
    {
        builder
            .Property(node => node.CodeName)
            .HasMaxLength(32)
            .IsRequired()
            .IsUnicode(false);

        builder
            .Property(node => node.DisplayName)
            .HasMaxLength(32)
            .IsRequired()
            .IsUnicode(false);
    }

    protected override void ConfigureIndex(
        EntityTypeBuilder<NodeEntity> builder)
        => builder
            .HasIndex(entity => entity.CodeName)
            .IsUnique();
}