using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FIT5032_Assignment_v1.Models
{
    public partial class FIT5032_Models : DbContext
    {
        public FIT5032_Models()
            : base("name=FIT5032_Models")
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Dentist> Dentists { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dentist>()
                .Property(e => e.AggregatedRating)
                .HasPrecision(2, 1);

            modelBuilder.Entity<Dentist>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.Dentist)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.Location)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.Dentists)
                .WithRequired(e => e.Location)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.Patient)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dentist>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.Dentist)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.Patient)
                .WillCascadeOnDelete(false);
        }
    }
}
