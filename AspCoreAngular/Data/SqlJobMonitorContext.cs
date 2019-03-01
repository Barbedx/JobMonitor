using System;
using JobMonitor.BLL.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AspCoreAngular.Data
{
    public partial class SqlJobMonitorContext : IdentityDbContext<ApplicationUser>
    { 
        public SqlJobMonitorContext(DbContextOptions<SqlJobMonitorContext> options)
            : base(options)
        {
             Database.EnsureCreated();//???

        }

        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<SqlServer> Servers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
              

            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasKey(e => e.Guid)
                    .HasName("PK__tblJobs__497F6CB4915B0B62");

                entity.ToTable("tblJobs");

                entity.Property(e => e.Guid)
                    .HasColumnName("guid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Frequency)
                    .HasColumnName("frequency")
                    .HasMaxLength(100);

                entity.Property(e => e.IsRunning).HasColumnName("isRunning");

                entity.Property(e => e.IsScheduled).HasColumnName("isScheduled");

                entity.Property(e => e.JobCategory)
                    .HasColumnName("jobCategory")
                    .HasMaxLength(200);

                entity.Property(e => e.JobEnabled).HasColumnName("jobEnabled");

                entity.Property(e => e.JobOwner)
                    .HasColumnName("jobOwner")
                    .HasMaxLength(200);

                entity.Property(e => e.LastOutcomeMessage).HasColumnName("lastOutcomeMessage");

                entity.Property(e => e.LastRunCommand)
                    .IsRequired()
                    .HasColumnName("lastRunCommand");

                entity.Property(e => e.LastRunDate)
                    .HasColumnName("lastRunDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.LastRunDuration)
                    .HasColumnName("LastRunDuration")
                    .HasConversion(new TimeSpanToTicksConverter());


                entity.Property(e => e.LastRunOutcome).HasColumnName("lastRunOutcome");

                entity.Property(e => e.LastRunStepMessage)
                    .IsRequired()
                    .HasColumnName("lastRunStepMessage");

                entity.Property(e => e.LastRunStepName)
                    .IsRequired()
                    .HasColumnName("lastRunStepName")
                    .HasMaxLength(200);

                entity.Property(e => e.LastRunStepNumber).HasColumnName("lastRunStepNumber");

                entity.Property(e => e.MaxDuration)
                .HasColumnName("MaxDuration")
                .HasConversion(new TimeSpanToTicksConverter());

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(200);

                entity.Property(e => e.NextRunDate)
                    .HasColumnName("nextRunDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.NumberOfSteps).HasColumnName("numberOfSteps");

                entity.Property(e => e.Recurrence)
                    .HasColumnName("recurrence")
                    .HasMaxLength(100);

                entity.Property(e => e.SheduleName)
                    .HasColumnName("sheduleName")
                    .HasMaxLength(200);

                entity.Property(e => e.SqlServerPath)
                    .IsRequired()
                    .HasColumnName("sqlServerPath")
                    .HasMaxLength(200);

                entity.Property(e => e.SubdayFrequency)
                    .HasColumnName("subdayFrequency")
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.SqlServer)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.SqlServerPath)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblJobs_tblServers");
            });

            modelBuilder.Entity<SqlServer>(entity =>
            {
                entity.HasKey(e => e.SqlServerPath);

                entity.ToTable("tblServers");

                entity.Property(e => e.SqlServerPath)
                    .HasColumnName("sqlServerPath")
                    .HasMaxLength(200)
                    .ValueGeneratedNever();

                entity.Property(e => e.IsEnabled).HasColumnName("isEnabled");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole("Admin"), new IdentityRole("User")
                );
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser() { Email = "my@email.com", SecurityStamp = Guid.NewGuid().ToString(), UserName = "myname" }
                );


            base.OnModelCreating(modelBuilder);

            //var context = serviceProvider.GetRequiredService<SqlJobMonitorContext>();
            //var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();



        }


    }
}
