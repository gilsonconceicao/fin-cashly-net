using FinCashly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Type)
            .IsRequired()
            .HasConversion<int>(); // enum

        builder.Property(x => x.IsDefault)
            .IsRequired();

        builder.HasMany(x => x.Transactions)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId);

        builder.HasMany(x => x.Goals)
            .WithOne()
            .OnDelete(DeleteBehavior.NoAction);
    }
}
