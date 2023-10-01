using Fs.Exceptions;

namespace Fs.Interfaces;

public interface IFileSystemService
{
    /// <summary>
    /// Загрузить файл в файловое хранилище.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="fileContent"></param>
    /// <exception cref="MetaDataRepositoryException">Не удалось добавить мета данные</exception>
    /// <exception cref="InvalidFileSizeException">Файл пустой</exception>
    /// <exception cref="StorageException">Не удалось загрузить</exception>
    /// <returns>Access Key</returns>
    Task<string> Upload(string fileName, IFormFile fileContent);
}