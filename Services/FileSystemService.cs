using Fs.Entities;
using Fs.Exceptions;
using Fs.Interfaces;

namespace Fs.Services;

/// <summary>
/// Основной сервис для работы с файлами
/// </summary>
public class FileSystemService : IFileSystemService
{
    private readonly IStorageRepository _storage;
    private readonly IMetaDataRepository _metaDataRepository;
    private readonly ILogger<FileSystemService> _logger;

    public FileSystemService(
        IStorageRepository storage, 
        IMetaDataRepository metaDataRepository,
        ILogger<FileSystemService> logger
    )
    {
        _storage = storage;
        _metaDataRepository = metaDataRepository;
        _logger = logger;
    }
    
    public async Task<string> Upload(string fileName, IFormFile fileContent)
    {
        var fileWithSameNameAlreadyExists = await _metaDataRepository.Exists(fileName);
        if (fileWithSameNameAlreadyExists)
            throw new FileAlreadyExistsException();
        
        var sameFilePath = await _storage.GetFilePathWithSameContent(fileContent);
        // На диске уже существует файл с таким содержимым, возвращаем его ключ в бд
        if (sameFilePath is not null)
        {
            var accessKey = await _metaDataRepository.GetAccessKeyByFilePath(sameFilePath);
            if (accessKey is null)
            {
                _logger.LogError("Unable to find meta data for file from disk");
                throw new InternalException();
            }
        }
        
        var path = await _storage.Upload(fileName, fileContent);
        var metaData = new FileMetaData
        {
            Name = fileName,
            Path = path,
            AccessKey = Guid.NewGuid()
        };
        await _metaDataRepository.Add(metaData);
        
        return metaData.AccessKey.ToString();
    }
}