using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AssistiveRobot.Web.Service.Domains
{
    public partial class AssistiveRobotContext : DbContext
    {
        public AssistiveRobotContext()
        {
        }

        public AssistiveRobotContext(DbContextOptions<AssistiveRobotContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Goal> Goal { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        // public virtual DbSet<UserToken> UserToken { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Goal>(entity =>
            {
                entity.Property(e => e.Status)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Goal)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Goal__JobId__4222D4EF");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Goal)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Goal__LocationId__4316F928");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OrientationW).HasColumnType("decimal(10, 7)");

                entity.Property(e => e.OrientationX).HasColumnType("decimal(10, 7)");

                entity.Property(e => e.OrientationY).HasColumnType("decimal(10, 7)");

                entity.Property(e => e.OrientationZ).HasColumnType("decimal(10, 7)");

                entity.Property(e => e.PositionX).HasColumnType("decimal(10, 7)");

                entity.Property(e => e.PositionY).HasColumnType("decimal(10, 7)");

                entity.Property(e => e.PositionZ).HasColumnType("decimal(10, 7)");
            });

            // modelBuilder.Entity<UserToken>(entity =>
            // {
            //     entity.HasNoKey();

            //     entity.Property(e => e.CheckSum).IsUnicode(false);

            //     entity.Property(e => e.ClientId)
            //         .HasMaxLength(50)
            //         .IsUnicode(false);

            //     entity.Property(e => e.Expires).HasColumnType("datetime");

            //     entity.Property(e => e.Issued).HasColumnType("datetime");

            //     entity.Property(e => e.Nonce).IsUnicode(false);

            //     entity.Property(e => e.RefreshToken).IsUnicode(false);

            //     entity.Property(e => e.UserId).ValueGeneratedOnAdd();
            // });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__1788CC4CE8E59729");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
