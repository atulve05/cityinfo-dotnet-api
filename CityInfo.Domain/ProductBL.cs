using System.Diagnostics;
using CityInfo.Data.Entities;
using CityInfo.Domain.Interface;
using Microsoft.Extensions.Logging;
using CityInfo.Data.Services.Interface;

namespace CityInfo.Domain
{
    public class ProductBL : IProductBL
    {
        private readonly ILogger<ProductBL> _logger;
        private readonly ICarvedRockRepository _repo;
        public ProductBL(ILogger<ProductBL> logger, ICarvedRockRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public async Task<ProductEntity?> GetProductByIdAsync(int id)
        {
            return await _repo.GetProductByIdAsync(id);
        }
        public async Task<IEnumerable<ProductEntity>> GetProductsForCategoryAsync(string category)
        {
            _logger.LogInformation("Getting products in logic for {category}", category);

            Activity.Current?.AddEvent(new ActivityEvent("Getting products from repository"));
            var results = await _repo.GetProductsAsync(category);
            Activity.Current?.AddEvent(new ActivityEvent("Retrieved products from repository"));

            return results;
        }
        public ProductEntity? GetProductById(int id)
        {
            _logger.LogDebug("Logic for single product ({id})", id);
            return _repo.GetProductById(id);
        }
        public IEnumerable<ProductEntity> GetProductsForCategory(string category)
        {
            return _repo.GetProducts(category);
        }
    }
}
