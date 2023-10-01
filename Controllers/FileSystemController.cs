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
    [SwaggerResponse(400, Description = "Неверный размер или название")]
    [SwaggerResponse(500, Description = "Ошибка на стороне сервера")]
    public async Task<ActionResult<string>> UploadFile(string fileName, [FromForm] IFormFile file)
    {
        string? accessKey;

        try
        {
            accessKey = await _fileSystem.Upload(fileName, file);
        }
        catch (InvalidFileNameException)
        {
            return BadRequest();
        }
        catch (InvalidFileSizeException)
        {
            return BadRequest();
        }
        catch (Exception)
        {
            return new StatusCodeResult(500);
        }
        
        return Ok(accessKey);
    }
    
    [HttpGet("access-key")]
    [SwaggerOperation("Получение ключа по имени файла")]
    [SwaggerResponse(200)]
    [SwaggerResponse(404, Description = "Файл не существует")]
    [SwaggerResponse(500, Description = "Ошибка на стороне сервера")]
    public async Task<ActionResult<string>> GetFileAccessKeyByName(string fileName)
    {
        string? accessKey;

        try
        {
            accessKey = await _fileSystem.GetFileAccessKeyByName(fileName);
        }
        catch (Exception)
        {
            return new StatusCodeResult(500);
        }

        if (accessKey is null)
            return NotFound();
        
        return Ok(accessKey);
    }
    
    [HttpGet("file")]
    [SwaggerOperation("Получение содержимого файла по ключу доступа")]
    [SwaggerResponse(200)]
    [SwaggerResponse(404, Description = "Файл не существует")]
    [SwaggerResponse(500, Description = "Ошибка на стороне сервера")]
    public async Task<ActionResult<FileContentResult>> GetFileByKey(string accessKey)
    {
        FileContentResult? file;

        try
        {
            file = await _fileSystem.GetFileContentByAccessKey(accessKey);
        }
        catch (Exception)
        {
            return new StatusCodeResult(500);
        }

        if (file is null)
            return NotFound();
        
        return Ok(accessKey);
    }

}