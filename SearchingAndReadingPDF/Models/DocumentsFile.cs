using System.ComponentModel.DataAnnotations;

namespace SearchingAndReadingPDF.Models
{
    public class DocumentsFile
    {
        [Key]
        public int ID { get; set; }
        public string FileName { get; set; }
        public string OBJNumber { get; set; }
        public string APPNumber { get; set; }
        public string Valuer { get; set; }
    }
}
