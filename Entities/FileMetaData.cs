using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fs.Entities;

[Table("files")]
public class FileMetaData
{
    /// <summary>
    /// Ключ доступа к файлу
    /// </summary>
    [Key]
    [Column("access_key")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid AccessKey { get; set; }
    
    /// <summary>
    /// Название файла
    /// </summary>
    [Column("name")]
    public string Name { get; set; }

    /// <summary>
    /// Путь до содержимого файла в файловой системе
    /// </summary>
    [Column("path")]
    public string Path { get; set; }
}