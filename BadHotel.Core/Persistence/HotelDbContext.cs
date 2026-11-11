using Microsoft.EntityFrameworkCore;

namespace BadHotel.Core.Persistence;

public class HotelDbContext : DbContext
{
    public HotelDbContext(DbContextOptions<HotelDbContext> options)
        : base(options)
    {
    }

    public DbSet<WorkerEntity> Workers => Set<WorkerEntity>();

    public DbSet<GroupEntity> Groups => Set<GroupEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<GroupEntity>(entity =>
        {
            entity.ToTable("Groups");

            entity.HasKey(group => group.Id);

            entity.Property(group => group.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity.HasIndex(group => group.Name)
                .IsUnique();
        });

        modelBuilder.Entity<WorkerEntity>(entity =>
        {
            entity.ToTable("Workers");

            entity.HasKey(worker => worker.Id);

            entity.HasDiscriminator(worker => worker.Type)
                .HasValue<WorkerEntity>("Worker")
                .HasValue<ReceptionistEntity>("Receptionist")
                .HasValue<CleanerEntity>("Cleaner")
                .HasValue<CookEntity>("Cook");

            entity.Property(worker => worker.Type)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(worker => worker.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(worker => worker.Energy)
                .IsRequired();

            entity.ToTable(table =>
                table.HasCheckConstraint(
                    "CK_Workers_Energy",
                    "[Energy] BETWEEN 0 AND 100"));

            entity.HasOne(worker => worker.Group)
                .WithMany(group => group.Workers)
                .HasForeignKey(worker => worker.GroupId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ReceptionistEntity>();
        modelBuilder.Entity<CleanerEntity>();
        modelBuilder.Entity<CookEntity>();
    }
}
