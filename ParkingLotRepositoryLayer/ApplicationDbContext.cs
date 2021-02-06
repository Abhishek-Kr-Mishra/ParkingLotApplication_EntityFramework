using Microsoft.EntityFrameworkCore;
using ParkingLotModelLayer;
using System;

namespace ParkingLotRepositoryLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<ParkingDetails> ParkingDetails { get; set; }
        public DbSet<DriverTypeDetails> DriverTypeDetails { get; set; }
        public DbSet<ParkingTypeDetails> ParkingTypeDetails { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<VehicleTypeDetails> VehicleTypeDetails { get; set; }

    }
}
