using CityInfo.Data.Entities;

namespace CityInfo.Data.Services.Interface
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<CityEntity>> GetCityAsync();
        Task<CityEntity?> GetCityAsync(int cityId, bool includePointsOfInterest);
        Task<(IEnumerable<CityEntity>, PaginationMetadata)> GetCityAsync(string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<bool> CityExistsAsync(int cityId);
        Task<bool> CityNameMatchesCityId(string? cityName, int cityId);
        Task<IEnumerable<PointOfInterestEntity>> GetPointsOfInterestForCityAsync(int cityId);
        Task<PointOfInterestEntity?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);
        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterestEntity pointOfInterest);
        void DeletePointOfInterest(PointOfInterestEntity pointOfInterest);
        Task<bool> SaveChangesAsync();
    }
}