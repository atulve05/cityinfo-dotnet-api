using CityInfo.Api.Dto;
using CityInfo.Domain.Models;

namespace CityInfo.Api.Mapper
{
    public static class ApiMappingExtensions
    {
        public static IEnumerable<CityWithoutPointsOfInterestDto> ToCityWithoutPointsOfInterestDtos(
            this IEnumerable<City> cities)
        {
            return cities.Select(city => city.ToCityWithoutPointsOfInterestDto());
        }

        public static CityWithoutPointsOfInterestDto ToCityWithoutPointsOfInterestDto(this City city)
        {
            return new CityWithoutPointsOfInterestDto
            {
                Id = city.Id,
                Name = city.Name,
                Description = city.Description
            };
        }

        public static CityDto ToCityDto(this City city)
        {
            return new CityDto
            {
                Id = city.Id,
                Name = city.Name,
                Description = city.Description,
                PointsOfInterest = city.PointsOfInterest
                    .Select(point => point.ToPointOfInterestDto())
                    .ToList()
            };
        }

        public static IEnumerable<PointOfInterestDto> ToPointOfInterestDtos(
            this IEnumerable<PointOfInterest> pointsOfInterest)
        {
            return pointsOfInterest.Select(point => point.ToPointOfInterestDto());
        }

        public static PointOfInterestDto ToPointOfInterestDto(this PointOfInterest pointOfInterest)
        {
            return new PointOfInterestDto
            {
                Id = pointOfInterest.Id,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };
        }

        public static PointOfInterestRequest ToRequest(this PointOfInterestForCreationDto dto)
        {
            return new PointOfInterestRequest
            {
                Name = dto.Name,
                Description = dto.Description
            };
        }
    }
}
