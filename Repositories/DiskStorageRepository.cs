using Fs.Exceptions;
using Fs.Interfaces;

namespace Fs.Repositories;

/// <summary>
/// Дисковое хранилище файлов
/// </summary>
public class DiskStorageRepository : IStorageRepository
{
    private readonly ILogger<DiskStorageRepository> _logger;
    private readonly IFileContentComparer _bytesComparer;
    private readonly string _rootPath;
    
    public DiskStorageRepository(
        IConfiguration configuration, 
        ILogger<DiskStorageRepository> logger,
        IFileContentComparer bytesComparer
    )
    {
        _logger = logger;
        _bytesComparer = bytesComparer;
        var rootPath = configuration.GetValue<string>("StoragePath") ;

        if (string.IsNullOrEmpty(rootPath))
            throw new ArgumentException("Invalid StoragePath");
        if (!Directory.Exists(rootPath))
            throw new ArgumentException("StoragePath directory ({0}) does not exist", rootPath);

        _rootPath = rootPath;
    }
    
    public async Task<string> Upload(string fileName, IFormFile fileContent)
    {
        if (fileContent.Length is <= 0 or > Const.BytesInOneGb) 
            throw new InvalidFileSizeException();
        
        var filePath = Path.Combine(_rootPath, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        
        try
        {
            await fileContent.CopyToAsync(stream);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Cannot upload file");
            throw new StorageException();
        }

        _logger.LogInformation("Uploaded new file {FilePath}", filePath);

        return filePath;
    }
    
    private async Task<byte[]?> GetBytes(string filePath)
    {
        byte[]? fileBytes;
            
        try
        {
            fileBytes = await File.ReadAllBytesAsync(filePath);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Cannot read bytes from path: {Path}", filePath);
            throw new StorageException();
        }

        return fileBytes;
    }

    public async Task<string?> GetFilePathWithSameContent(IFormFile fileContent)
    {
        var allFilesInDisk = GetAllFilePaths();
        
        await using var stream = new MemoryStream((int)fileContent.Length);
        await fileContent.CopyToAsync(stream);
        var bytesInRequest = stream.ToArray();
        
        foreach (var filePath in allFilesInDisk)
        {
            var bytesInDisk = await GetBytes(filePath);
            if (bytesInDisk is null)
                continue;

            if (_bytesComparer.AreFileContentsEqual(bytesInRequest, bytesInDisk))
                return filePath;
        }

        return null;
    }

    private IEnumerable<string> GetAllFilePaths()
    {
        return Directory.GetFiles(_rootPath, "*.*", SearchOption.AllDirectories);
    }
}