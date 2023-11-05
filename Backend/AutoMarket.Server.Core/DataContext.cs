using AutoMarket.Server.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoMarket.Server.Core
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Seed();
            base.OnModelCreating(builder);

            builder.Entity<Car>()
                .HasOne(p => p.Modification)
                .WithMany(c => c.Cars)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<ModelGeneration>()
                .HasKey(mg => new { mg.ModelId, mg.GenerationId });

            builder.Entity<ModelGeneration>()
                .HasOne(g => g.Model)
                .WithMany(m => m.ModelGenerations)
                .HasForeignKey(mg => mg.ModelId);

            builder.Entity<ModelGeneration>()
                .HasOne(g => g.Generation)
                .WithMany(m => m.ModelGenerations)
                .HasForeignKey(mg => mg.GenerationId);
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<BodyType> BodyTypes { get; set; }
        public DbSet<GearBoxType> GearBoxes { get; set; }
        public DbSet<DriveTrain> DriveTrains { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<ProducingCountry> ProducingCountries { get; set; }
        public DbSet<Modification> Modifications { get; set; }
        public DbSet<Generation> Generations { get; set; }
        public DbSet<ModelGeneration> ModelGeneration { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<TechnicalCondition> TechnicalConditions { get; set;}
    }
}