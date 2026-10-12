using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyError.Model;

namespace MyError.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentFilterController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create(Student student)
        {
            // Если код выполняется, валидация уже пройдена.
            // Здесь сохранение пользователя в БД.

            return StatusCode(201, new
            {
                message = "Студент создан"
            });
        }
    }
}