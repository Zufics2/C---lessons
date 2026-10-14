using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyError.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcController : ControllerBase
    {
        [HttpGet("GetData")]
        public ActionResult GetData(string date)
        {
            return Ok(DateTime.Parse(date));
        }
        
        [HttpGet("Divide")]
        public ActionResult Divide(int a, int b)
        {
            int result = a / b;
            return Ok(result);
        }
        
        [HttpGet("User/{id}")]
        public ActionResult GetUser(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "User ID must be greater than 0");
            }

            if (id == 999)
            {
                throw new KeyNotFoundException($"User {id} not found");
            }

            return Ok(new { Id = id, Name = "Test user" });
        }
        
        [HttpGet("Process")]
        public ActionResult ProcessData(bool isReady)
        {
            if (!isReady)
            {
                throw new InvalidOperationException("Operation not yet ready");
            }

            return Ok("Data is ready");
        }
    }
}