using ComputerGamesORM.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComputerGamesORM.Data.Configurations;

public sealed class GameDescriptionConfiguration : IEntityTypeConfiguration<GameDescription>
{
    public void Configure(EntityTypeBuilder<GameDescription> builder)
    {
        builder.ToTable("GameDescriptions");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .ValueGeneratedOnAdd();

        builder.Property(d => d.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(d => d.GameId)
            .IsRequired();

        builder.HasIndex(d => d.GameId)
            .IsUnique()
            .HasDatabaseName("IX_GameDescriptions_GameId");

        builder.HasOne(d => d.Game)
            .WithOne(g => g.GameDescription)
            .HasForeignKey<GameDescription>(d => d.GameId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
