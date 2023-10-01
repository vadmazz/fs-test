namespace Fs.Interfaces;

/// <summary>
/// Абстракция над разными хранилищами данных
/// </summary>
public interface IStorageRepository
{
    /// <summary>
    /// Загрузить файл в файловое хранилище.
    /// </summary>
    /// <param name="fileName">Название файла</param>
    /// <param name="fileContent">Содержимое файла</param>
    /// <returns>File path</returns>
    Task<string> Upload(string fileName, IFormFile fileContent);

    /// <summary>
    /// Содержимое уже существует
    /// </summary>
    /// <param name="fileContent">Содержимое с которым проверяем</param>
    /// <returns>File path до такого же файла</returns>
    Task<string?> GetFilePathWithSameContent(IFormFile fileContent);
}