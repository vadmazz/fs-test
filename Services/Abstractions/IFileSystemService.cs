namespace Fs.Services.Abstractions;

public interface IFileSystemService
{
    /// <summary>
    /// Загрузить файл в файловое хранилище.
    /// </summary>
    /// <param name="fileName">Название файла</param>
    /// <param name="fileContent">Содержимое файла</param>
    /// <returns>Ключ доступа</returns>
    Task<string> Upload(string fileName, IFormFile fileContent);
}