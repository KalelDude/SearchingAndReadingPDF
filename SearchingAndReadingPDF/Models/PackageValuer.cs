using System.ComponentModel.DataAnnotations;

namespace SearchingAndReadingPDF.Models
{
    public class PackageValuer
    {
        [Key]
        public int PackageValuerID { get; set; }

        public string PackName { get; set; }

        public string ObjectNo { get; set; }

        public string? ValuerName { get; set; }
        
        
    }
}
