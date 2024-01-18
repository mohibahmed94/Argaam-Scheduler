using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ArgaamSchedular.Entities;

public partial class ArgaamPlusContext : DbContext
{
    public ArgaamPlusContext()
    {
    }

    public ArgaamPlusContext(DbContextOptions<ArgaamPlusContext> options)
        : base(options)
    {
    }

    public virtual DbSet<JobType> JobTypes { get; set; }

    public virtual DbSet<ScheduledJob> ScheduledJobs { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JobType>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.JobTypeId).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<ScheduledJob>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PK__Schedule__056690C2AEA608F3");

            entity.Property(e => e.Frequency).HasMaxLength(150);
            entity.Property(e => e.ScheduleTime).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
