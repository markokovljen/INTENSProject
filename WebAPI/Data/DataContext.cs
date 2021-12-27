using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<JobCandidate> JobCandidates { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<JobCandidateSkill> JobCandidateSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobCandidateSkill>()
                .HasKey(cs => new { cs.JobCandidateId, cs.SkillId });
            modelBuilder.Entity<JobCandidateSkill>()
                .HasOne(cs => cs.JobCandidate)
                .WithMany(c => c.JobCandidateSkills)
                .HasForeignKey(cs => cs.JobCandidateId);
            modelBuilder.Entity<JobCandidateSkill>()
                .HasOne(cs => cs.Skill)
                .WithMany(s => s.JobCandidateSkills)
                .HasForeignKey(cs => cs.SkillId);
        }
    }
}
