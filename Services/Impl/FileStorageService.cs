using Fs.Exceptions;
using Fs.Services.Abstractions;

namespace Fs.Services.Impl;

public class FileStorageService : IFileStorageService
{
    private readonly ILogger<FileStorageService> _logger;
    private readonly string _rootPath;
    
    public FileStorageService(IConfiguration configuration, ILogger<FileStorageService> logger)
    {
        _logger = logger;
        var rootPath = configuration.GetValue<string>("StoragePath") ;

        if (string.IsNullOrEmpty(rootPath))
            throw new ArgumentException("Invalid StoragePath");
        if (!Directory.Exists(rootPath))
            throw new ArgumentException("StoragePath directory ({0}) does not exist", rootPath);

        _rootPath = rootPath;
    }
    
    public async Task Upload(string fileName, IFormFile fileContent)
    {
        if (fileContent.Length <= 0) 
            throw new FileIsEmptyException();
        
        var filePath = Path.Combine(_rootPath, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        
        try
        {
            await fileContent.CopyToAsync(stream);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Cannot upload file");
            throw new UnableUploadFileException();
        }

        _logger.LogInformation("Uploaded new file {FilePath}", filePath);
    }
}