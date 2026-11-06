using Asp.Versioning;
using CityInfo.Api.Dto;
using CityInfo.Api.Mapper;
using Microsoft.AspNetCore.Mvc;
using CityInfo.Domain.Interface;
using Microsoft.AspNetCore.Authorization;

namespace CityInfo.Api.Controllers
{
    [ApiController]
    //[ApiVersion("1.0")]
    [Route("api/cities/{cityId}/pointsofinterest")]
    [Authorize(Policy = "MustBeFromBengaluru")]
    public class PointOfInterestController : ControllerBase
    {
        private readonly ICityInfoBL _cityInfo;
        private readonly ILogger<PointOfInterestController> _logger;

        public PointOfInterestController(ICityInfoBL cityInfo, ILogger<PointOfInterestController> logger)
        {
            this._cityInfo = cityInfo;
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointOfInterest(int cityId)
        {
            //var cityName = User.Claims.FirstOrDefault(c => c.Type == "city")?.Value;
            //if (!await _cityInfoRepository.CityNameMatchesCityId(cityName, cityId))
            //    return Forbid();

            if (!await _cityInfo.IsCityExists(cityId))
                return NotFound();

            var pointsOfInterest = await _cityInfo.GetPointsOfInterestForCityAsync(cityId);

            return Ok(pointsOfInterest.ToPointOfInterestDtos());
        }

        [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            if (!await _cityInfo.IsCityExists(cityId))
                return NotFound();

            var pointOfInterest = await _cityInfo.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterest == null)
                return NotFound();

            return Ok(pointOfInterest.ToPointOfInterestDto());

        }

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId, PointOfInterestForCreationDto pointOfInterestDto)
        {
            //APIController doing this no need to do this
            if (!ModelState.IsValid)
                return BadRequest();

            if (!await _cityInfo.IsCityExists(cityId))
                return NotFound();

            var pointOfInterest = pointOfInterestDto.ToRequest();

            var response = await _cityInfo.CreatePointOfInterestForCityAsync(cityId, pointOfInterest);

            if (response == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            var createdPointOfInterestToReturn = response.ToPointOfInterestDto();

            return CreatedAtRoute("GetPointOfInterest", new
            {
                cityId = cityId,
                pointOfInterestId = createdPointOfInterestToReturn.Id
            }, createdPointOfInterestToReturn);
        }

        [HttpDelete("{pointOfInterestId}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Tags("AA","BB")]
        public async Task<ActionResult> DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            if (!await _cityInfo.IsCityExists(cityId))
                return NotFound();

            var deleted = await _cityInfo.DeletePointOfInterest(cityId, pointOfInterestId);

            if (!deleted)
                return NotFound();

            //_mailService.Send(
            //    "Point of interest deleted.",
            //    $"Point of interest {pointOfInterestEntity.Name} with id {pointOfInterestEntity.Id} was deleted.");
            return NoContent();
        }
    }
}
