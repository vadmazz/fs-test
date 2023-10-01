using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fs.Entities;

[Table("files")]
public class FileMetaData
{
    [Key]
    [Column("access_key")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid AccessKey { get; set; }

    /// <summary>
    /// Размер файла в мегабайтах
    /// </summary>
    [Column("size")]
    public double Size { get; set; }
    
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