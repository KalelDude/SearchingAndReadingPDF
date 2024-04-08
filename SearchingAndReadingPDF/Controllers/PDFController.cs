using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SearchingAndReadingPDF.Data;
using SearchingAndReadingPDF.Models;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using static NuGet.Packaging.PackagingConstants;

namespace SearchingAndReadingPDF.Controllers
{
    public class PDFController : Controller
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration _config;
        private readonly FileDataBaseContext _db;
        private string mainfilePath = @"C:\\users\\10114636\\Downloads\\";
        //private string mainfilePath = @"C:\\Users\\user\\Downloads\\";

        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();

        List<Township> Township_List = new List<Township>();

        public PDFController(ILogger<PDFController> logger, FileDataBaseContext db, Microsoft.Extensions.Configuration.IConfiguration config)
        {

            _db = db;
            _config = config;
            con.ConnectionString = _config.GetConnectionString("DefaultConnection");
        }
        

      

       

        public IActionResult showMvd ()
        {
            return View();
        }

       
        //Viewing files from the specific tabs 
        public IActionResult sectionsPage (string MvdName )
        {


            //Publish Main File Path
            //string mainfilePath = $"{_config["AppSettings:FileRooTPath"]}";
            List<string> sectionFolders = Directory.GetDirectories(mainfilePath + MvdName)
                                .Select(filePath => Path.GetFileName(filePath)).ToList();

            ViewBag.SectionNames = sectionFolders;
            TempData["MVDName"] = MvdName;

            
           

            var tabVariable = new TabVariable
            {
                pathname = MvdName,

              

            };

            return View(tabVariable);
        }


        //File search from the mvd page  
        public IActionResult searchPage(string sectionName, string MvdName, string folderName)
        {

            /* Publish Code 
             * if (Township_List.Count > 0)
            {
                Township_List.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = " EXEC [Objection].[dbo].[propertyDetailsTown]"; // WHERE [Sector] = '"+ userSector + "' OR [Sector] IS NULL
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    Township_List.Add(new Township()
                    {
                        TownshipNames = dr["TsOnlyName"].ToString()
                    });
                }
                con.Close();
                ViewBag.Township_List = Township_List.ToList();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message.ToString());               
                throw ex;
            }
            */



            if (Township_List.Count > 0)
            {
                Township_List.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "Select distinct TownshipNames  FROM [Section53Revised].[dbo].[TownShips]"; // WHERE [Sector] = '"+ userSector + "' OR [Sector] IS NULL
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    Township_List.Add(new Township()
                    {
                        TownshipNames = dr["TownshipNames"].ToString()
                    });
                }
                con.Close();
                ViewBag.Township_List = Township_List.ToList();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message.ToString());               
                throw ex;
            }

            TempData["sectionName"] = folderName;
            TempData["MVDsearchPage"] = MvdName;
            TempData["folderName"] = folderName;
            return View();
        }


        //Viewing files from the MVD folder
        public IActionResult viewFiles(string TownName, string ObjectionNo, string MvdName, string folderName)
        {
            //Publish Main File Path
            //string mainfilePath = $"{_config["AppSettings:FileRooTPath"]}";
            List<string> files = Directory.GetFiles(mainfilePath + MvdName + "\\" + folderName, "*.pdf")
                                .Select(filePath => Path.GetFileName(filePath)).ToList();

            List<string> filteredNames = new List<string>();
            if (TownName != null && ObjectionNo != null)
            {
                foreach (var file in files)
                {
                    if (file.Contains(TownName) && file.Contains(ObjectionNo))
                    {
                        filteredNames.Add(file);
                    }

                }
            }
            else if (TownName != null && ObjectionNo == null)
            {
                foreach (var file in files)
                {
                    if (file.Contains(TownName))
                    {
                        filteredNames.Add(file);
                    }

                }
            }



            var pathsForfiles = new ListAndPaths
            {
                MvdName=MvdName,
                foldername=folderName
            };
            

            ViewBag.TownNames = filteredNames;

            return View(pathsForfiles);
        }


        public IActionResult viewOBJPack(string packPath)
        {
            List<string> FHDirectory = Directory.GetDirectories(mainfilePath + packPath+"\\FH").Select(filePath => Path.GetFileName(filePath)).ToList();
            List<string> STDirectory = Directory.GetDirectories(mainfilePath + packPath + "\\ST")
                       .Select(filePath => Path.GetFileName(filePath)).ToList();


            List<string> FHLetterFilePath = new List<string>();
            List<string> STLetterFilePath = new List<string>(); 

            List<string> FinalPath = new List<string>(); 

           var combinedPath = new List<string>(); 

            //FH Directory
            foreach (var file in FHDirectory) 
            {
                FHLetterFilePath.Add(file);
            }

            //ST Directory
            foreach (var file in STDirectory)
            {
                STLetterFilePath.Add(file);
            }

            //ST Letter folder
            foreach (var file in STLetterFilePath)
            {
                FinalPath.AddRange(Directory.GetDirectories(mainfilePath + packPath + "\\ST\\" + file).
                   Select(filePath => Path.GetFileName(filePath)).ToList());
            }

            //FH Letter folder
            foreach (var file in FHLetterFilePath)
            {
                FinalPath.AddRange(Directory.GetDirectories(mainfilePath + packPath + "\\FH\\" + file).
                   Select(filePath => Path.GetFileName(filePath)).ToList());
            }


            TempData["ObjectionPack"] = packPath;
            Regex objRegex = new Regex(@"OBJ-[A-Za-z0-9]+-\d+");
            string OBJ = "";

            var packages = new PackageValuer();

            List<PackageValuer> list = new List<PackageValuer>();   

            foreach (var file in FinalPath)
            {
                Match objMatch = objRegex.Match(file);
                if (objMatch.Success) 
                {
                    OBJ = objMatch.Value;
                }



                packages = new PackageValuer
                {
                    PackName = file,
                    ObjectNo = OBJ,
                    ValuerName = ""

                };

                list.Add(packages); 
                
            }


            if (_db.PackageValuer.LongCount() <= 1)
            {
                _db.PackageValuer.AddRange(list);
                _db.SaveChanges();
            }

            //ViewBag.LetterArray = FHLetterFilePath;
            //ViewBag.LetterArray = FinalPath;

            ////FH Letter folder
            //foreach (var file in FHLetterFilePath)
            //{
            //    FinalPath = Directory.GetDirectories(mainfilePath + packPath + "\\FH\\" + file).
            //       Select(filePath => Path.GetFileName(filePath)).ToList(); 
            //}

            //ST Letter folder
            //foreach (var file in STLetterFilePath)
            //{
            //    FinalPath .AddRange(Directory.GetDirectories(mainfilePath + packPath + "\\ST\\" + file).
            //       Select(filePath => Path.GetFileName(filePath)).ToList());
            //}



            var packageList = _db.PackageValuer.ToList();



            //ViewBag.LetterArray= FHLetterFilePath;  


            return View(packageList);  
        }



        //Downloading pdf files 
        public FileResult DownloadDocument(string mvd,string folder,string file)
        {
            //Publish Main File Path
            //string mainfilePath = $"{_config["AppSettings:FileRooTPath"]}";

            string path = Path.Combine(mainfilePath + mvd + "\\" + folder + "\\", file);



            byte[] bytes = System.IO.File.ReadAllBytes(path);
           
            return File(bytes, "application/octet-stream",file);
        }



        //Reading pdf files
        public FileResult readPDF(string mvd,string folder , string file)
        {

            //Publish Main File Path
            //string mainfilePath = $"{_config["AppSettings:FileRooTPath"]}";
            string path = Path.Combine(mainfilePath + mvd + "\\" + folder + "\\", file);


            byte[] FileBytes = System.IO.File.ReadAllBytes(path);
            return File(FileBytes, "application/pdf");

        }


        //Zipping and downloading files 
        public FileResult DownloadBatch(string batchPath,string batchFile)
        {

            //Publish Main File Path
            //string mainfilePath = $"{_config["AppSettings:FileRooTPath"]}";
            string folderPath = mainfilePath + batchPath + "\\" + batchFile; 
            
            ZipFile.CreateFromDirectory(folderPath,batchFile+".zip");

            string file = batchFile + ".zip";
                 
            byte[] files = System.IO.File.ReadAllBytes(file);
            System.IO.File.Delete(batchFile + ".zip");


            return File(files, "application/octet-stream", batchFile +".zip");
 

        }

        //Zipping and downloading files 
        public FileResult DownloadObjection(string packPath ,string objectionFile)
        {

            //Publish Main File Path
            //string mainfilePath = $"{_config["AppSettings:FileRooTPath"]}";



            List<string> FHDirectory = Directory.GetDirectories(mainfilePath + packPath + "\\FH")
                              .Select(filePath => Path.GetFileName(filePath)).ToList();
            List<string> STDirectory = Directory.GetDirectories(mainfilePath + packPath + "\\ST")
                       .Select(filePath => Path.GetFileName(filePath)).ToList();


            List<string> FHLetterFilePath = new List<string>();
            List<string> STLetterFilePath = new List<string>();

            string letterPath = "";
            

            //var combinedPath = new List<string>();

            ////FH Directory
            foreach (var path in FHDirectory)
            {
                if(Directory.Exists(mainfilePath + packPath + "\\FH\\"+path+"\\"+objectionFile))
                {
                    letterPath="FH\\"+path;   
                }
                
            }

            ////ST Directory
            foreach (var path in STDirectory)
            {
                if (Directory.Exists(mainfilePath + packPath + "\\ST\\" + path + "\\" + objectionFile))
                {
                    letterPath ="ST\\"+path;
                }
            }

           


            string folderPath = mainfilePath + packPath + "\\" +letterPath+"\\"+objectionFile;

            ZipFile.CreateFromDirectory(folderPath, objectionFile + ".zip");

            string file = objectionFile + ".zip";

            byte[] files = System.IO.File.ReadAllBytes(file);
            System.IO.File.Delete(objectionFile + ".zip");


            return File(files, "application/octet-stream", objectionFile + ".zip");


        }

        [HttpGet]
        public IActionResult assignValuer(int PackageID, string Filename)
        {
            return View();
        }



    }
}

