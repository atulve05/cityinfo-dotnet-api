using Asp.Versioning;
using System.Text.Json;
using CityInfo.Api.Dto;
using CityInfo.Api.Mapper;
using Microsoft.AspNetCore.Mvc;
using CityInfo.Domain.Interface;
using Microsoft.AspNetCore.Authorization;

namespace CityInfo.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/cities")]
    [Authorize]
    public class CityController : ControllerBase
    {
        private readonly ICityInfoBL _cityInfo;
        private readonly ILogger<CityController> _logger;
        const int maxCitiesPageSize = 20;

        public CityController(ICityInfoBL cityInfo, ILogger<CityController> logger)
        {
            this._cityInfo = cityInfo;
            this._logger = logger;
        }

        /// <summary>
        /// Get a list of cities with optional filtering and pagination
        /// </summary>
        /// <param name="name">The name of the city to filter by</param>
        /// <param name="searchQuery">A search query to filter cities</param>
        /// <param name="pageNumber">The page number for pagination</param>
        /// <param name="pageSize">The page size for pagination</param>
        /// <returns>A list of cities</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<CityWithoutPointsOfInterestDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInterestDto>>> GetCities(
           string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxCitiesPageSize)
                pageSize = maxCitiesPageSize;

            //var cityEntities = await _cityInfoRepository.GetCityAsync();
            var (cities, paginationMetadata) = await _cityInfo.GetCityInfo(name, searchQuery, pageNumber, pageSize);

            Response.Headers.Append("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(cities.ToCityWithoutPointsOfInterestDtos());
        }

        /// <summary>
        /// Get a city by id
        /// </summary>
        /// <param name="id"> the id of the city to get</param>
        /// <param name="includePointOfInterest">Whether or not to include the point of interest</param>
        /// <returns>An IActionResult</returns>
        /// <response code="200">Return the requested city</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCities(int id, bool includePointOfInterest = false)
        {
            var city = await _cityInfo.GetCityInfo(id, includePointOfInterest);
            if (city == null) return NotFound();

            if (includePointOfInterest)
                return Ok(city.ToCityDto());

            return Ok(city.ToCityWithoutPointsOfInterestDto());
        }
    }
}
