using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lesson23DataBaseFirst;

public partial class FootBallDatabaseFirstContext : DbContext
{
    public FootBallDatabaseFirstContext()
    {
    }

    public FootBallDatabaseFirstContext(DbContextOptions<FootBallDatabaseFirstContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Timetable> Timetables { get; set; }

    public virtual DbSet<Traner> Traners { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-522TD1V\\TEW_SQLEXPRESS;Initial Catalog=FootBallDatabaseFirst;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.TeamsId).HasColumnName("TeamsID");

            entity.HasOne(d => d.Teams).WithMany(p => p.Groups)
                .HasForeignKey(d => d.TeamsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Groups_Groups");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.ToTable("Player");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TeamsId).HasColumnName("TeamsID");

            entity.HasOne(d => d.Teams).WithMany(p => p.Players)
                .HasForeignKey(d => d.TeamsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Player_Teams");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.Traners).WithMany(p => p.Teams)
                .UsingEntity<Dictionary<string, object>>(
                    "TeamsTraner",
                    r => r.HasOne<Traner>().WithMany()
                        .HasForeignKey("TranerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TeamsTraner_Traners"),
                    l => l.HasOne<Team>().WithMany()
                        .HasForeignKey("TeamsId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TeamsTraner_Teams"),
                    j =>
                    {
                        j.HasKey("TeamsId", "TranerId");
                        j.ToTable("TeamsTraner");
                        j.HasIndex(new[] { "TeamsId", "TranerId" }, "IX_TeamsTraner").IsUnique();
                    });
        });

        modelBuilder.Entity<Timetable>(entity =>
        {
            entity.ToTable("Timetable");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.TeamIda).HasColumnName("TeamIDA");
            entity.Property(e => e.TeamIdb).HasColumnName("TeamIDB");

            entity.HasOne(d => d.Group).WithMany(p => p.Timetables)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Timetable_Groups1");

            entity.HasOne(d => d.TeamIdaNavigation).WithMany(p => p.TimetableTeamIdaNavigations)
                .HasForeignKey(d => d.TeamIda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Timetable_Timetable");

            entity.HasOne(d => d.TeamIdbNavigation).WithMany(p => p.TimetableTeamIdbNavigations)
                .HasForeignKey(d => d.TeamIdb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Timetable_Teams");
        });

        modelBuilder.Entity<Traner>(entity =>
        {
            entity.HasIndex(e => new { e.Name, e.Surname }, "IX_Traners").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
