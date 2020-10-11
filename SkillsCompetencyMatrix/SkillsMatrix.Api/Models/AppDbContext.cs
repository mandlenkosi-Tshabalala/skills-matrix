using Microsoft.EntityFrameworkCore;
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Competency> Competencies { get; set; }
        public DbSet<CompetencyCategory> CompetencyCatagories { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<EmploymentHistory> EmploymentHistories { get; set; }
        public DbSet<Expertise> Expertises { get; set; }
        public DbSet<ExpertiseCategory> ExpertiseCategories { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<ProfessionalMembership> ProfessionalMemberships { get; set; }
        public DbSet<Reference> References { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
