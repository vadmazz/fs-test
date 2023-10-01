using Fs.Interfaces;

namespace Fs.Services;

public class FileContentComparer : IFileContentComparer
{
    public bool AreFileContentsEqual(byte[] file1, byte[] file2)
    {
        if (file1.Length != file2.Length)
            return false;
        
        // Не уверен что это лучший способ проверки
        // поэтому выделил интерфейс для быстрой замены 
        return file1.SequenceEqual(file2);
    }
}