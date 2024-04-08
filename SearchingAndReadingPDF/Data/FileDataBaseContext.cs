using Microsoft.EntityFrameworkCore;
using SearchingAndReadingPDF.Models;

namespace SearchingAndReadingPDF.Data
{
    public class FileDataBaseContext : DbContext
    {
        public FileDataBaseContext (DbContextOptions<FileDataBaseContext> options) : base(options) 
        {

        }
       // public DbSet<DocumentsFile> documentsTable { get; set; }
        public DbSet <Township> TownShips { get; set; }

        public DbSet <PackageValuer> PackageValuer { get; set; }


    }
}
