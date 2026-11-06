using CityInfo.Data.Entities;

namespace CityInfo.Data.Services.Interface
{
    public interface ICarvedRockRepository
    {
        Task<ProductEntity?> GetProductByIdAsync(int id);
        Task<List<ProductEntity>> GetProductsAsync(string category);
        ProductEntity? GetProductById(int id);
        List<ProductEntity> GetProducts(string category);  
    }
}
