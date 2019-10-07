using Almotkaml.ArtificialLimbs.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core
{
    public class ArtificialLimbsDbContext : DbContext
    {
        public int LoggedInUserId { get; internal set; }
        public ArtificialLimbsDbContext()
            : base("ConnectionString")
        {

        }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<CompanyInfos> CompanyInfos { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Nationality> Nationalitys { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Hospitals> Hospitals { get; set; }
        public DbSet<AmputationTypes> AmputationTypes { get; set; }
        public DbSet<Infoes> Infoes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<PatientReport> PatientReports { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<DailyStatues> DailyStatues { get; set; }
        public DbSet<DailyDetails> DailyDetails { get; set; }
        public DbSet<AmputationStatues> AmputationStatues { get; set; }
        public DbSet<AmputationDetails> AmputationDetails { get; set; }
        public DbSet<AmputationReason> AmputationReasons { get; set; }

        public static ArtificialLimbsDbContext Create()
        {
            return new ArtificialLimbsDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Infoes>().HasKey(k => k.Name)
           .Property(s => s.Value).HasMaxLength(MidField);

            //modelBuilder.Entity<Patient>().HasRequired(a => a.City)
            //.WithMany()
            //.WillCascadeOnDelete(false);

            //  modelBuilder.Entity<DailyStatues>()
            // .HasRequired<Employee>(s => s.Technical)
            // .WithMany(g => g.DailyStatues)
            // .HasForeignKey(s => s.TechnicalID);

            //  modelBuilder.Entity<DailyStatues>()
            //.HasRequired<Employee>(s => s.TechnicalAssistant)
            //.WithMany(g => g.DailyStatues)
            //.HasForeignKey(s => s.TechnicalAssistantID);

            //  modelBuilder.Entity<AmputationStatues>()
            //.HasRequired<Employee>(s => s.Technical)
            //.WithMany(g => g.AmputationStatues)
            //.HasForeignKey(s => s.TechnicalID);

            //  modelBuilder.Entity<AmputationStatues>()
            //.HasRequired<Employee>(s => s.TechnicalAssistant)
            //.WithMany(g => g.AmputationStatues)
            //.HasForeignKey(s => s.TechnicalAssistantID);

            modelBuilder.Entity<Employee>()
             .HasMany(c => c.DailyStatues1)
             .WithOptional(c => c.Technical)
             .HasForeignKey(c => c.TechnicalID).WillCascadeOnDelete(false);

            //modelBuilder.Entity<DailyStatues>()
            //         .HasOptional(m => m.Technical)
            //         .WithMany(t => t.DailyStatues)
            //         .HasForeignKey(m => m.TechnicalID)
            //         .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
            .HasMany(c => c.DailyStatues)
            .WithOptional(c => c.TechnicalAssistant)
            .HasForeignKey(c => c.TechnicalAssistantID).WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
            .HasMany(c => c.AmputationStatues1)
            .WithOptional(c => c.Technical)
            .HasForeignKey(c => c.TechnicalID).WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
            .HasMany(c => c.AmputationStatues)
            .WithOptional(c => c.TechnicalAssistant)
            .HasForeignKey(c => c.TechnicalAssistantID).WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);


        }

        #region helpers
        public static string DateCreated => "_" + nameof(DateCreated);
        public static string DateModified => "_" + nameof(DateModified);
        public static string CreatedBy => "_" + nameof(CreatedBy);
        public static string ModifiedBy => "_" + nameof(ModifiedBy);
        public static string Column(string name) => "_" + name;
        public static int SmallField => 128;
        public static int MidField => 1000;
        public static int BigField => 4000;
        public static string DecimalField => "decimal(18,10)";

        public object CurrentSituations { get; internal set; }


        #endregion
    }
}