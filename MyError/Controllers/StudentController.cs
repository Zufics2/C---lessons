using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using MyError.Model;

namespace MyError.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IValidator<Student> _validator;
    
    public StudentController(IValidator<Student> validator)
    {
        _validator = validator;
    }

    [HttpPost("StudentAdd")]
    public async Task<ActionResult> StudentAddAsync(Student student, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(student, cancellationToken);

        if (!result.IsValid)
        {
            var errors = result.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray());

            return BadRequest(new ValidationProblemDetails
            {
                Errors = errors,
                Status = StatusCodes.Status400BadRequest,
                Title = "Ошибка валидации"
            });
        }

        // Здесь сохранение пользователя в БД.

        return StatusCode(201, new
        {
            message = "Пользователь создан"
        });

    }
}
