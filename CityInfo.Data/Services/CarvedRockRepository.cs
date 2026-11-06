using System.Diagnostics;
using Microsoft.Data.Sqlite;
using CityInfo.Data.Entities;
using CityInfo.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CityInfo.Data.Services.Interface;

namespace CityInfo.Data.Services
{
    public class CarvedRockRepository : ICarvedRockRepository
    {
        private readonly CityInfoContext _ctx;
        private readonly ILogger<CarvedRockRepository> _logger;
        private readonly ILogger _factoryLogger;

        public CarvedRockRepository(CityInfoContext ctx, ILogger<CarvedRockRepository> logger,
            ILoggerFactory loggerFactory)
        {
            _ctx = ctx;
            _logger = logger;
            _factoryLogger = loggerFactory.CreateLogger("DataAccessLayer");
        }

        public async Task<ProductEntity?> GetProductByIdAsync(int id)
        {
            return await _ctx.Products.FindAsync(id);
        }
        public async Task<List<ProductEntity>> GetProductsAsync(string category)
        {
            _logger.LogInformation("Getting products in repository for {category}", category);
            if (category == "clothing")
            {
                var ex = new ApplicationException("Database error occurred!!");
                ex.Data.Add("Category", category);
                throw ex;
            }
            if (category == "equip")
            {
                throw new SqliteException("Simulated fatal database error occurred!", 551);
            }

            try
            {
                return await _ctx.Products.Where(p => p.Category == category || category == "all").ToListAsync();
            }
            catch (Exception ex)
            {
                var newEx = new ApplicationException("Something bad happened in database", ex);
                newEx.Data.Add("Category", category);
                throw newEx;
            }
        }
        public ProductEntity? GetProductById(int id)
        {
            var timer = new Stopwatch();
            timer.Start();

            var product = _ctx.Products.Find(id);
            timer.Stop();

            _logger.LogDebug("Querying products for {id} finished in {milliseconds} milliseconds",
                id, timer.ElapsedMilliseconds);

            _factoryLogger.LogInformation("(F) Querying products for {id} finished in {ticks} ticks",
                id, timer.ElapsedTicks);

            return product;
        }

        public List<ProductEntity> GetProducts(string category)
        {
            return _ctx.Products.Where(p => p.Category == category || category == "all").ToList();
        }
    }
}
