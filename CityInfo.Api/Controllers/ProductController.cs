using CityInfo.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using CityInfo.Domain.Interface;

namespace CityInfo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductBL _productLogic;

        //[LoggerMessage(LogEvents.GettingProducts, LogLevel.Information,
        //   "SourceGenerated - Getting products in API.")]
       
        //[LoggerMessage(LogLevel.Information, "SourceGenerated - Getting products in API.")]
        //partial void LogGettingProducts();

        public ProductController(ILogger<ProductController> logger, IProductBL productLogic)
        {
            _logger = logger;
            _productLogic = productLogic;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductEntity>> Get(string category = "all")
        {
            using (_logger.BeginScope("ScopeCat: {ScopeCat}", category))
            {
                //LogGettingProducts();
                //_logger.LogInformation(CarvedRockEvents.GettingProducts, "Getting products in API.");
                return await _productLogic.GetProductsForCategoryAsync(category);
            }
            //return _productLogic.GetProductsForCategory(category);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ProductEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            //var product = await _productLogic.GetProductByIdAsync(id);
            _logger.LogDebug("Getting single product in API for {id}", id);
            var product = _productLogic.GetProductById(id);
            if (product != null)
            {
                return Ok(product);
            }
            _logger.LogWarning("No product found for ID: {id}", id);
            return NotFound();
        }
    }
}
