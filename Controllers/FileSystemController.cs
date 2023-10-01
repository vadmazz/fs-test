using Fs.Models;
using Fs.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Fs.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("v{version:apiVersion}")]
[Tags("Файловая система")]
public class FileSystemController
{
    private readonly IFileStorageService _fileStorage;

    public FileSystemController(IFileStorageService fileStorage)
    {
        _fileStorage = fileStorage;
    }
    
    [HttpPost]
    [SwaggerOperation("Загрузить файл")]
    [SwaggerResponse(200, Description = "Файл загружен")]
    [SwaggerResponse(500)]
    public async Task<ActionResult<ApiResponse<string>>> UploadFile(string fileName, [FromForm] IFormFile file)
    {
        await _fileStorage.Upload(file);
        return Ok(new ApiResponse<string>(link));
    }
}