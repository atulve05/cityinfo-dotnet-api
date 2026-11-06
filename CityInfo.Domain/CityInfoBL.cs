using CityInfo.Domain.Models;
using CityInfo.Data.Services;
using CityInfo.Domain.Interface;
using CityInfo.Domain.Mapper;
using CityInfo.Data.Services.Interface;
using CityInfo.Data.Entities;

namespace CityInfo.Domain
{
    public class CityInfoBL : ICityInfoBL
    {
        private readonly ICityInfoRepository _cityInfoRepository;

        public CityInfoBL(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            var cityEntities = await _cityInfoRepository.GetCityAsync();
            return cityEntities.ToDomain();
        }

        public async Task<City?> GetCityInfo(int cityId, bool includePointsOfInterest)
        {
            var city = await _cityInfoRepository.GetCityAsync(cityId, includePointsOfInterest);
            return city?.ToDomain();
        }

        public async Task<(IEnumerable<City>, PaginationMetadata)> GetCityInfo(string? name, string? searchQuery, int pageNumber, int pageSize)
        {
            var (cityEntities, paginationMetadata) = await _cityInfoRepository
             .GetCityAsync(name, searchQuery, pageNumber, pageSize);

            return (cityEntities.ToDomain(), paginationMetadata);
        }

        public async Task<bool> IsCityExists(int cityId)
        {
            return await _cityInfoRepository.CityExistsAsync(cityId);
        }

        public Task<bool> IsCityNameMatchesCityId(string? cityName, int cityId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
        {
            var pointOfInterests = await _cityInfoRepository.GetPointsOfInterestForCityAsync(cityId);
            return pointOfInterests.ToDomain();
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
        {
            var pointOfInterest = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            return pointOfInterest?.ToDomain();
        }

        public async Task<PointOfInterest?> CreatePointOfInterestForCityAsync(int cityId, PointOfInterestRequest pointOfInterest)
        {
            var pointOfInterestEntity = pointOfInterest.ToEntity();
            await _cityInfoRepository.AddPointOfInterestForCityAsync(cityId, pointOfInterestEntity);
            var result = await _cityInfoRepository.SaveChangesAsync();
            if (result)
                return pointOfInterestEntity.ToDomain();
            else
                return null;
        }

        public async Task<bool> DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            var pointOfInterest = await _cityInfoRepository
                .GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterest == null)
                return false;

            _cityInfoRepository.DeletePointOfInterest(pointOfInterest);
            return await _cityInfoRepository.SaveChangesAsync();
        }
    }
}
