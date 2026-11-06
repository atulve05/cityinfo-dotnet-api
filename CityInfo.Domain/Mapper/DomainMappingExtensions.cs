using CityInfo.Data.Entities;
using CityInfo.Domain.Models;

namespace CityInfo.Domain.Mapper
{
    public static class DomainMappingExtensions
    {
        public static City ToDomain(this CityEntity entity)
        {
            return new City
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                PointsOfInterest = entity.PointsOfInterest
                    .Select(point => point.ToDomain())
                    .ToList()
            };
        }

        public static IEnumerable<City> ToDomain(this IEnumerable<CityEntity> entities)
        {
            return entities.Select(entity => entity.ToDomain());
        }

        public static PointOfInterest ToDomain(this PointOfInterestEntity entity)
        {
            return new PointOfInterest
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
        }

        public static IEnumerable<PointOfInterest> ToDomain(this IEnumerable<PointOfInterestEntity> entities)
        {
            return entities.Select(entity => entity.ToDomain());
        }

        public static PointOfInterestEntity ToEntity(this PointOfInterestRequest request)
        {
            return new PointOfInterestEntity(request.Name)
            {
                Description = request.Description
            };
        }
    }
}
