namespace Common.Data.Contexts.Configurations;

using Common.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TemplateEntityTypeConfiguration : BaseEntityTypeConfiguration<int, TemplateEntity>
{
    public override void ConfigureColumns(
        EntityTypeBuilder<TemplateEntity> builder)
    {
        builder
            .Property(template => template.Name)
            .HasMaxLength(32)
            .IsRequired()
            .IsUnicode(false);
        builder
            .Property(template => template.Summary)
            .HasMaxLength(128)
            .IsRequired()
            .IsUnicode(true);
    }

    protected override void ConfigureIndex(
        EntityTypeBuilder<TemplateEntity> builder)
        => builder
            .HasIndex(template => template.Name)
            .IsUnique();
}