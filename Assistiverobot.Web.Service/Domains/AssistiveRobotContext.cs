using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Assistiverobot.Web.Service.Domains
{
    public partial class AssistiveRobotContext : DbContext
    {
        public AssistiveRobotContext()
        {
        }

        public AssistiveRobotContext(DbContextOptions<AssistiveRobotContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Goal> Goal { get; set; }
        public virtual DbSet<Job> Job { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=AssistiveRobot;Trusted_Connection=False;User ID=sa;Password=P@ssw0rd");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Goal>(entity =>
            {
                entity.Property(e => e.OrientationW).HasColumnType("decimal(10, 7)");

                entity.Property(e => e.OrientationX).HasColumnType("decimal(10, 7)");

                entity.Property(e => e.OrientationY).HasColumnType("decimal(10, 7)");

                entity.Property(e => e.OrientationZ).HasColumnType("decimal(10, 7)");

                entity.Property(e => e.PositionX).HasColumnType("decimal(10, 7)");

                entity.Property(e => e.PositionY).HasColumnType("decimal(10, 7)");

                entity.Property(e => e.PositionZ).HasColumnType("decimal(10, 7)");

                entity.Property(e => e.Status)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Goal)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK__Goal__JobId__38996AB5");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
