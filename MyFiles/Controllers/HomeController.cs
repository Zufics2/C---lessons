using System.Data;
using System.Globalization;
using System.Text;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace MyFiles.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly IDbConnection _db;

    public HomeController(IDbConnection db)
    {
        _db = db;
    }

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
    
    [HttpPost("UploadMulty")]
    public async Task<IActionResult> UploadMulty(List<IFormFile> files)
    {
        if (files == null || files.Count == 0)
            return BadRequest("Файлы не выбраны");

        var folder = Path.Combine(AppContext.BaseDirectory, "Files");

        Directory.CreateDirectory(folder);

        var uploadedFiles = new List<string>();

        foreach (var file in files)
        {
            if (file.Length == 0)
                continue;

            var extension = Path.GetExtension(file.FileName);
            
            var fileName = $"{Guid.NewGuid()}{extension}";

            var filePath = Path.Combine(folder, fileName);

            await using var stream = new FileStream(
                filePath,
                FileMode.Create
            );

            await file.CopyToAsync(stream);

            uploadedFiles.Add(fileName);
        }

        return Ok(uploadedFiles);
    }
    
    [HttpPost("UploadXLSX")]
    public async Task<IActionResult> UploadXLSX(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Файл не выбран");

        var extension = Path.GetExtension(file.FileName);

        if (!extension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            return BadRequest("Можно загрузить только Excel-файл (.xlsx)");

        var folder = Path.Combine(AppContext.BaseDirectory, "Files");

        Directory.CreateDirectory(folder);

        var fileName = $"{Guid.NewGuid()}{extension}";

        var filePath = Path.Combine(folder, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return Ok(new
        {
            FileName = fileName,
            Path = filePath
        });
    }
    
    [HttpGet("DownloadTXT")]
    public IActionResult DownloadTxt()
    {
        string text = "Привет!\r\nЭто динамический TXT-файл.";

        var bytes = System.Text.Encoding.UTF8.GetBytes(text);

        return File(
            bytes,
            "text/plain",
            "result.txt"
        );
    }
    
    [HttpGet("DownloadFile")]
    public IActionResult DownloadFile(string fileName)
    {
        var folder = Path.Combine(AppContext.BaseDirectory, "Files");

        var filePath = Path.Combine(folder, fileName);

        if (!System.IO.File.Exists(filePath))
            return NotFound("Файл не найден");

        var contentType = Path.GetExtension(fileName).ToLowerInvariant() switch
        {
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".xls" => "application/vnd.ms-excel",

            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".doc" => "application/msword",

            ".pdf" => "application/pdf",

            ".txt" => "text/plain",

            ".csv" => "text/csv",

            ".zip" => "application/zip",

            _ => "application/octet-stream"
        };

        return PhysicalFile(
            filePath,
            contentType,
            fileName
        );
    }

    [HttpGet("DownloadCSV")]
    public async Task<IActionResult> DownloadCSV(string? fileName = null)
    {
        var rows = (await _db.QueryAsync("SELECT Id, Name, Email, CreatedAt FROM Users"))
            .Cast<IDictionary<string, object>>()
            .ToList();

        if (rows.Count == 0)
            return NotFound("Пользователи не найдены");

        var sb = new StringBuilder();

        var columns = rows[0].Keys.ToList();
        sb.AppendLine(string.Join(",", columns.Select(EscapeCsv)));

        foreach (var row in rows)
        {
            var values = columns.Select(c => EscapeCsv(FormatCsvValue(row[c])));
            sb.AppendLine(string.Join(",", values));
        }
        
        var utf8 = new UTF8Encoding(true);
        var bytes = utf8.GetPreamble()
            .Concat(utf8.GetBytes(sb.ToString()))
            .ToArray();
        
        fileName = string.IsNullOrWhiteSpace(fileName) ? "users.csv" : Path.GetFileName(fileName);

        if (!fileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            fileName += ".csv";

        return File(bytes, "text/csv", fileName);
    }

    private static string FormatCsvValue(object? value) => value switch
    {
        null => "",
        DateTime dt => dt.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
        IFormattable f => f.ToString(null, CultureInfo.InvariantCulture),
        _ => value.ToString() ?? ""
    };
    
    private static string EscapeCsv(string field)
    {
        if (field.IndexOfAny(new[] { ',', '"', '\r', '\n' }) >= 0)
            return "\"" + field.Replace("\"", "\"\"") + "\"";

        return field;
    }
}