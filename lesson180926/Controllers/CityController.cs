using AutoMapper;
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
        private readonly ICity service;
        private readonly IMapper _mapper;
        
        public CityController(ICity service, IMapper mapper)
        {
            this.service = service;
            _mapper = mapper;
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

        [HttpGet("Test")]
        public IActionResult Test()
        {
            var model1 = new Model1
            {
                a = "AAA",
                b = "BBB"
            };

            var model2 = _mapper.Map<Model2>(model1);

            return Ok(model2);
        }

        [HttpGet("Test2")]
        public IActionResult Test2()
        {
            var model3 = new Model3
            {
                a = "Иван",
                b = "Описание"
            };

            var model4 = _mapper.Map<Model4>(model3);
            return Ok(model4);
        }

        [HttpGet("Test3")]
        public IActionResult Test3()
        {
            var model5 = new Model5
            {
                Name = "Gabar",
                BirthDate = new DateTime(2009, 2, 10),
                Amount = 100,
                Description = "Test description"
            };

            var model6 = _mapper.Map<Model6>(model5);
            
            return Ok(model6);
        }

        [HttpGet("Test4")]
        public IActionResult Test4()
        {
            List<Model5> startList = new List<Model5>
            {
                new Model5
                {
                    Name = "Gabar1", BirthDate = new DateTime(2009, 2, 10), Amount = 100,
                    Description = "Test description1"
                },
                new Model5
                {
                    Name = "Gabar2", BirthDate = new DateTime(2009, 02, 11), Amount = 250,
                    Description = "Test description2"
                },
            };
            
            List<Model6> destinationList = _mapper.Map<List<Model6>>(startList);
            return Ok(destinationList);
        }
    }
}
