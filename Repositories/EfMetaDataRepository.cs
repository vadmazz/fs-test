using Fs.Entities;
using Fs.Exceptions;
using Fs.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fs.Repositories;

/// <summary>
/// Хранилище мета данных с использованием Entity Framework
/// </summary>
public class EfMetaDataRepository : IMetaDataRepository, IDisposable
{
    private readonly CoreContext _dbContext;
    private readonly ILogger<EfMetaDataRepository> _logger;
    
    public EfMetaDataRepository(CoreContext dbContext, ILogger<EfMetaDataRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task Add(FileMetaData metaData)
    {
        try
        {
            await _dbContext.Files.AddAsync(metaData);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unable to add meta data to db");
            throw new MetaDataRepositoryException();
        }
    }
    
    public async Task<string?> GetFileAccessKeyByName(string fileName)
    {
        string? accessKey;
        
        try
        {
            accessKey = (await _dbContext.Files
                    .AsNoTracking()
                    .Select(f => new { AccessKey = f.AccessKey, Name = f.Name })
                    .FirstOrDefaultAsync(f => f.Name == fileName))
                ?.AccessKey.ToString();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unable to find access key by fileName: {FileName}", fileName);
            throw new MetaDataRepositoryException();
        }

        return accessKey;
    }

    public async Task<bool> Exists(string fileName)
    {
        return await _dbContext.Files.AnyAsync(f => f.Name == fileName);
    }

    public async Task<string?> GetAccessKeyByFilePath(string path)
    {
        string? accessKey;
        
        try
        {
            accessKey = (await _dbContext.Files
                .AsNoTracking()
                .Select(f => new { AccessKey = f.AccessKey, Path = f.Path })
                .FirstOrDefaultAsync(f => f.Path == path))?.AccessKey.ToString();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unable to find access key by path: {Path}", path);
            throw new MetaDataRepositoryException();
        }

        return accessKey;
    }

    public async Task<string?> GetPathByAccessKey(string accessKey)
    {
        string? path;
        
        try
        {
            path = (await _dbContext.Files
                .AsNoTracking()
                .Select(f => new { AccessKey = f.AccessKey, Path = f.Path })
                .FirstOrDefaultAsync(f => f.AccessKey.ToString() == accessKey))?.Path.ToString();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unable to find Path by access key: {AccessKey}", accessKey);
            throw new MetaDataRepositoryException();
        }

        return path;
    }

    private bool _disposed;
 
    public virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            _disposed = true;
        }
    }
 
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}