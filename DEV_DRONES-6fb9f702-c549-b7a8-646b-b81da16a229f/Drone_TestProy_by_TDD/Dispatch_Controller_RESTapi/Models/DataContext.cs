using Dispatch_Controller_RESTapi.Enums;
using Microsoft.EntityFrameworkCore;

namespace Dispatch_Controller_RESTapi.Models
{
    public class DataContext:DbContext
    {
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = DroneDB.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModelEntity>()
            .HasData(
                Enum.GetValues(typeof(ModelEnum))
                    .Cast<ModelEnum>()
                    .Select(x => new ModelEntity
                    {
                        Id = (int)x,
                        Name = x.ToString()
                    })
            );

            modelBuilder.Entity<StateEntity>()
            .HasData(
                Enum.GetValues(typeof(StateEnum))
                .Cast<StateEnum>()
                .Select(x => new StateEntity
                {
                    Id = (int)x,
                    Name = x.ToString()
                })
            );

            modelBuilder.Entity<DroneMedicationsEntity>().HasKey(x => new { x.DroneId, x.MedicationId });
        }


        public DbSet<DroneEntity> Drones{ get;set; }
        public DbSet<MedicationEntity> Medications{ get; set; }
        public DbSet<ModelEntity> DroneModel { get; set; }
        public DbSet<StateEntity> States { get; set; }
        public DbSet<DroneMedicationsEntity> DroneMedications{ get; set; }


    }
}