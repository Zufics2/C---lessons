using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lesson180926.Abstract;
using lesson180926.Model;

namespace lesson180926.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        ICity service;
        public CityController(ICity service)
        {
            this.service = service;
        }

        [HttpGet("CityGetAll")]
        public ActionResult CityGetAll()
        {
            return Ok(service.CityGetAll());
        }

        [HttpGet("CityDelete/{id}")]
        public ActionResult CityDelete(int id)
        {
            return Ok(service.CityDelete(id));
        }

        [HttpPost("CityAdd")]
        public ActionResult CityAdd([FromBody] CityDTO city)
        {
            string result = service.CityAdd(city);
            return Ok(result);
        }
    }
}
