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
            // User Address
            modelBuilder.Entity<User>()
                .ToTable("AspNetUsers")
                .HasOne(a => a.Address)
                .WithOne(a => a.User)
                .HasForeignKey<Address>(c => c.UserId);

            // User Personal Info
            modelBuilder.Entity<User>()
                .HasOne(a => a.PersonalInfo)
                .WithOne(a => a.User)
                .HasForeignKey<PersonalInfo>(c => c.UserId);

            // User Competency
            modelBuilder.Entity<UserCompetency>()
                .HasKey(x => new { x.UserId, x.CompetencyId });

            modelBuilder.Entity<UserCompetency>()
                .HasOne<User>(uc => uc.User)
                .WithMany(s => s.UserCompetencies)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserCompetency>()
                .HasOne<Competency>(uc => uc.Competency)
                .WithMany(s => s.UserCompetencies)
                .HasForeignKey(uc => uc.CompetencyId);

            // User Document
            modelBuilder.Entity<UserDocument>()
               .HasKey(x => new { x.UserId, x.DocumentId });

            modelBuilder.Entity<UserDocument>()
                .HasOne<User>(uc => uc.User)
                .WithMany(s => s.UserDocuments)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserDocument>()
                .HasOne<Document>(uc => uc.Document)
                .WithMany(s => s.UserDocuments)
                .HasForeignKey(uc => uc.DocumentId);

            // User Education
            modelBuilder.Entity<UserEducation>()
               .HasKey(x => new { x.UserId, x.EducationId });

            modelBuilder.Entity<UserEducation>()
                .HasOne<User>(uc => uc.User)
                .WithMany(s => s.UserEducations)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserEducation>()
                .HasOne<Education>(uc => uc.Education)
                .WithMany(s => s.UserEducations)
                .HasForeignKey(uc => uc.EducationId);

            // User Employment
            modelBuilder.Entity<UserEmployment>()
               .HasKey(x => new { x.UserId, x.EmploymentId });

            modelBuilder.Entity<UserEmployment>()
                .HasOne<User>(uc => uc.User)
                .WithMany(s => s.UserEmployments)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserEmployment>()
                .HasOne<Employment>(uc => uc.Employment)
                .WithMany(s => s.UserEmployments)
                .HasForeignKey(uc => uc.EmploymentId);

            // User Expertise
            modelBuilder.Entity<UserExpertise>()
               .HasKey(x => new { x.UserId, x.ExpertiseId });

            modelBuilder.Entity<UserExpertise>()
                .HasOne<User>(uc => uc.User)
                .WithMany(s => s.UserExpertises)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserExpertise>()
                .HasOne<Expertise>(uc => uc.Expertise)
                .WithMany(s => s.UserExpertises)
                .HasForeignKey(uc => uc.ExpertiseId);

            // User Industry
            modelBuilder.Entity<UserIndustry>()
               .HasKey(x => new { x.UserId, x.IndustryId });

            modelBuilder.Entity<UserIndustry>()
                .HasOne<User>(uc => uc.User)
                .WithMany(s => s.UserIndustries)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserIndustry>()
                .HasOne<Industry>(uc => uc.Industry)
                .WithMany(s => s.UserIndustries)
                .HasForeignKey(uc => uc.IndustryId);

            // User Membership
            modelBuilder.Entity<UserMembership>()
               .HasKey(x => new { x.UserId, x.MembershipId });

            modelBuilder.Entity<UserMembership>()
                .HasOne<User>(uc => uc.User)
                .WithMany(s => s.UserMemberships)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserMembership>()
                .HasOne<Membership>(uc => uc.Membership)
                .WithMany(s => s.UserMemberships)
                .HasForeignKey(uc => uc.MembershipId);

            // User Skill
            modelBuilder.Entity<UserSkill>()
               .HasKey(x => new { x.UserId, x.SkillId });

            modelBuilder.Entity<UserSkill>()
                .HasOne<User>(uc => uc.User)
                .WithMany(s => s.UserSkills)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserSkill>()
                .HasOne<Skill>(uc => uc.Skill)
                .WithMany(s => s.UserSkills)
                .HasForeignKey(uc => uc.SkillId);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Competency> Competencies { get; set; }
        public DbSet<CompetencyCategory> CompetencyCatagories { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Employment> Employments { get; set; }
        public DbSet<Expertise> Expertises { get; set; }
        public DbSet<ExpertiseCategory> ExpertiseCategories { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<PersonalInfo> PersonalInfos { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Reference> References { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<UserCompetency> UserCompetencies { get; private set; }
        public DbSet<UserEducation> UserEducations { get; private set; }
        public DbSet<UserEmployment> UserEmployments { get; private set; }
        public DbSet<UserExpertise> UserExpertises { get; private set; }
        public DbSet<UserIndustry> UserIndustries { get; private set; }
        public DbSet<UserMembership> UserMemberships { get; private set; }
        public DbSet<UserDocument> UserDocuments { get; private set; }
    }
}
