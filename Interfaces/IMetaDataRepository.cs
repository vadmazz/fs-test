using Fs.Entities;
using Fs.Exceptions;

namespace Fs.Interfaces;

/// <summary>
/// Хранилище мета данных о файле
/// </summary>
public interface IMetaDataRepository
{
    /// <summary>
    /// Добавить
    /// </summary>
    /// <param name="metaData">Meta data</param>
    /// <exception cref="MetaDataRepositoryException">Внутрення ошибка с репозиторием (базой данных)</exception>
    Task Add(FileMetaData metaData);

    /// <summary>
    /// Ищем access key по имени файла
    /// </summary>
    /// <param name="fileName">Имя файла</param>
    /// <exception cref="MetaDataRepositoryException">Внутрення ошибка с репозиторием (базой данных)</exception>
    Task<string?> GetFileAccessKeyByName(string fileName);
    
    /// <summary>
    /// Существует ли файл с указанным именем
    /// </summary>
    /// <param name="fileName">Имя файла</param>
    Task<bool> Exists(string fileName);

    Task<string?> GetAccessKeyByFilePath(string path);
    Task<string?> GetPathByAccessKey(string accessKey);
}