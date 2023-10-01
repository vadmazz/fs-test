namespace Fs.Services.Abstractions;

public interface IFileStorageService
{
    /// <summary>
    /// Загрузить файл в файловое хранилище.
    /// </summary>
    /// <param name="fileName">Название файла</param>
    /// <param name="fileContent">Содержимое файла</param>
    /// <returns></returns>
    Task Upload(string fileName, IFormFile fileContent);
}