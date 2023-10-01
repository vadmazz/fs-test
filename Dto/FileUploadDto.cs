namespace Fs.Models;

public class FileUploadDto
{
    public IFormFile Content { get; set; }
    public string Name { get; set; }
}