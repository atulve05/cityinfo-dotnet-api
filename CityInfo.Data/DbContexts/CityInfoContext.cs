using CityInfo.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Data.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<CityEntity> Cities { get; set; } = null!;
        public DbSet<PointOfInterestEntity> PointsOfInterest { get; set; } = null!;
        public DbSet<ProductEntity> Products { get; set; } = null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CityEntity>()
                .HasData(
               new CityEntity("New York City")
               {
                   Id = 1,
                   Description = "The one with that big park."
               },
               new CityEntity("Antwerp")
               {
                   Id = 2,
                   Description = "The one with the cathedral that was never really finished."
               },
               new CityEntity("Paris")
               {
                   Id = 3,
                   Description = "The one with that big tower."
               });

            modelBuilder.Entity<PointOfInterestEntity>()
             .HasData(
               new PointOfInterestEntity("Central Park")
               {
                   Id = 1,
                   CityId = 1,
                   Description = "The most visited urban park in the United States."
               },
               new PointOfInterestEntity("Empire State Building")
               {
                   Id = 2,
                   CityId = 1,
                   Description = "A 102-story skyscraper located in Midtown Manhattan."
               },
               new PointOfInterestEntity("Cathedral")
               {
                   Id = 3,
                   CityId = 2,
                   Description = "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans."
               },
               new PointOfInterestEntity("Antwerp Central Station")
               {
                   Id = 4,
                   CityId = 2,
                   Description = "The the finest example of railway architecture in Belgium."
               },
               new PointOfInterestEntity("Eiffel Tower")
               {
                   Id = 5,
                   CityId = 3,
                   Description = "A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel."
               },
               new PointOfInterestEntity("The Louvre")
               {
                   Id = 6,
                   CityId = 3,
                   Description = "The world's largest museum."
               }
               );
            modelBuilder.Entity<ProductEntity>()
             .HasData(
               new ProductEntity
               {
                   Id = 1,
                   Name = "Trailblazer",
                   Category = "boots",
                   Price = 69.99,
                   Description = "Great support in this high-top to take you to great heights and trails.",
                   ImgUrl = "https://www.pluralsight.com/content/dam/pluralsight2/teach/author-tools/carved-rock-fitness/img-brownboots.jpg"
               },
               new ProductEntity
               {
                   Id = 2,
                   Name = "Coastliner",
                   Category = "boots",
                   Price = 49.99,
                   Description =
                    "Easy in and out with this lightweight but rugged shoe with great ventilation to get your around shores, beaches, and boats.",
                   ImgUrl = "https://www.pluralsight.com/content/dam/pluralsight2/teach/author-tools/carved-rock-fitness/img-greyboots.jpg"
               },
               new ProductEntity
               {
                   Id = 3,
                   Name = "Woodsman",
                   Category = "boots",
                   Price = 64.99,
                   Description =
                    "All the insulation and support you need when wandering the rugged trails of the woods and backcountry.",
                   ImgUrl = "/images/shutterstock_222721876.jpg"
               },
               new ProductEntity
               {
                   Id = 4,
                   Name = "Billy",
                   Category = "boots",
                   Price = 79.99,
                   Description =
                    "Get up and down rocky terrain like a billy-goat with these awesome high-top boots with outstanding support.",
                   ImgUrl = "/images/shutterstock_475046062.jpg"
               });
            base.OnModelCreating(modelBuilder);
        }
    }
}
