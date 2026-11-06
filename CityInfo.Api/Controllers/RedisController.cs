using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controllers
{
    public class RedisController : ControllerBase
    {
        //private readonly IRedisBL _repo;

        //public PlatformsController(IRedisBL repo)
        //{
        //    _repo = repo;
        //}

        //[HttpGet("{id}", Name = "GetPlatformById")]
        //public ActionResult<RedisPlatformEntity> GetPlatformById(string id)
        //{
        //    var platform = _repo.GetPlatformById(id);
        //    if (platform != null)
        //        return Ok(platform);

        //    return NotFound();
        //}

        //[HttpGet]
        //public ActionResult<IEnumerable<RedisPlatformEntity>> GetAllPlatforms()
        //{
        //    return Ok(_repo.GetAllPlatforms());
        //}

        //[HttpPost]
        //public ActionResult<RedisPlatformEntity> CreatePlatform(RedisPlatformEntity platform)
        //{
        //    _repo.CreatePlatform(platform);

        //    return CreatedAtRoute(nameof(GetPlatformById), new { Id = platform.Id }, platform);
        //}
    }
}
