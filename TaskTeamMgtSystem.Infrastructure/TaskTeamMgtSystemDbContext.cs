using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TaskTeamMgtSystem.Core.Domain.Entities;

namespace TaskTeamMgtSystem.Infrastructure
{
    public class TaskTeamMgtSystemDbContext : DbContext
    {
        public TaskTeamMgtSystemDbContext(DbContextOptions<TaskTeamMgtSystemDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<UserTeamMapping> UserTeamMappings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tasks: AssignedTo (User) and CreatedBy (User) with DeleteBehavior.Restrict
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.AssignedTo)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssignedToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.CreatedBy)
                .WithMany(u => u.CreatedTasks)
                .HasForeignKey(t => t.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tasks → Team with DeleteBehavior.Cascade
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.Team)
                .WithMany(team => team.Tasks)
                .HasForeignKey(t => t.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            // Team → TeamMembers (UserTeamMapping)
            modelBuilder.Entity<Team>()
                .HasMany(t => t.TeamMembers)
                .WithOne(utm => utm.Team)
                .HasForeignKey(utm => utm.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            // User ↔ UserTeamMapping
            modelBuilder.Entity<User>()
                .HasMany(u => u.TeamMappings)
                .WithOne(utm => utm.User)
                .HasForeignKey(utm => utm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
