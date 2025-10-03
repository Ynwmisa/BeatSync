using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BeatSync.Web.Models;

namespace BeatSync.Web.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Beat> Beats { get; set; }
    public DbSet<LicenseOption> LicenseOptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Beat>(entity =>
        {
            entity.Property(b => b.Title)
                  .HasMaxLength(200)
                  .IsRequired();
            entity.Property(b => b.Genre)
                  .HasMaxLength(100);
            entity.Property(b => b.Key)
                  .HasMaxLength(16);
            entity.Property(b => b.Mood)
                  .HasMaxLength(100);
            entity.Property(b => b.Instruments)
                  .HasMaxLength(300);
            entity.Property(b => b.AudioFilePath)
                  .HasMaxLength(400)
                  .IsRequired();
            entity.Property(b => b.CoverImagePath)
                  .HasMaxLength(400);
        });

        modelBuilder.Entity<LicenseOption>(entity =>
        {
            entity.HasIndex(l => new { l.BeatId, l.LicenseType }).IsUnique();
            entity.Property(l => l.Price)
                  .HasColumnType("decimal(10,2)")
                  .IsRequired();
        });
    }
}
