using Fs.Exceptions;
using Fs.Interfaces;
using Fs.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Fs.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("v{version:apiVersion}")]
[Tags("Файловая система")]
public class FileSystemController : ControllerBase
{
    private readonly IFileSystemService _fileSystem;

    public FileSystemController(IFileSystemService fileSystem)
    {
        _fileSystem = fileSystem;
    }
    
    [HttpPost]
    [SwaggerOperation("Загрузить файл")]
    [SwaggerResponse(200, Description = "Файл успешно загружен")]
    [SwaggerResponse(400, Description = "Неверный размер файла")]
    [SwaggerResponse(500, Description = "Ошибка на стороне сервера")]
    public async Task<ActionResult<ApiResponse<string>>> UploadFile(string fileName, [FromForm] IFormFile file)
    {
        string? accessKey;

        try
        {
            accessKey = await _fileSystem.Upload(fileName, file);
        }
        catch (InvalidFileSizeException)
        {
            return BadRequest();
        }
        catch (Exception)
        {
            return new StatusCodeResult(500);
        }
        
        return Ok(new ApiResponse<string>(accessKey));
    }
}