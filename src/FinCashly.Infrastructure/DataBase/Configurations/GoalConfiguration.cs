using FinCashly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class GoalConfiguration : IEntityTypeConfiguration<Goal>
{
    public void Configure(EntityTypeBuilder<Goal> builder)
    {
        builder.ToTable("Goals");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.TargetAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.CurrentAmount)
            .HasColumnType("decimal(18,2)");

        // 1:N -> User -> Goals
        builder.HasOne(x => x.User)
            .WithMany(x => x.Goals)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
