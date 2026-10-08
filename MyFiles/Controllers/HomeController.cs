using Microsoft.AspNetCore.Mvc;

namespace MyFiles.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    [HttpPost("Upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Файл не выбран");

        var folder = Path.Combine(AppContext.BaseDirectory, "Files");


        Directory.CreateDirectory(folder);

        var extension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{extension}";
        
        var filePath = Path.Combine(folder, fileName);

        await using var stream = new FileStream(
            filePath,
            FileMode.Create
        );

        await file.CopyToAsync(stream);

        return Created("Created", filePath);
    }
}