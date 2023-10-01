namespace Fs.Interfaces;

/// <summary>
/// Интерфейс описывает способ сравнения содержимого файла
/// </summary>
public interface IFileContentComparer
{
    bool AreFileContentsEqual(byte[] file1, byte[] file2);
}