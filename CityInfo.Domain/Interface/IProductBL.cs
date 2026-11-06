using CityInfo.Data.Entities;

namespace CityInfo.Domain.Interface
{
    public interface IProductBL
    {
        Task<ProductEntity?> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductEntity>> GetProductsForCategoryAsync(string category);
        ProductEntity? GetProductById(int id);
        IEnumerable<ProductEntity> GetProductsForCategory(string category);
        
    }
}
