using CityInfo.Data.Services;
using CityInfo.Domain.Models;

namespace CityInfo.Domain.Interface
{
    public interface ICityInfoBL
    {
        Task<IEnumerable<City>> GetCities();
        Task<City?> GetCityInfo(int cityId, bool includePointsOfInterest);
        Task<(IEnumerable<City>, PaginationMetadata)> GetCityInfo(string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<bool> IsCityExists(int cityId);
        Task<bool> IsCityNameMatchesCityId(string? cityName, int cityId);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);
        Task<PointOfInterest?> CreatePointOfInterestForCityAsync(int cityId, PointOfInterestRequest pointOfInterest);
        Task<bool> DeletePointOfInterest(int cityId, int pointOfInterestId);
    }
}
