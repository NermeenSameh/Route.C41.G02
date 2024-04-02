using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Route.C41.G02.PL.Helpers
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            // 1. Get Loacted Folder Path
            // string folderPath = $"C:\\Assignments\\Assignment 03 MVC\\Route.C41.G02\\Route.C41.G02.PL\\wwwroot\\files\\images\\{folderName}";
            //string folderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\files\\images\\{folderName}";
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // 2. Get FIle Name and Make it UNIQUE
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // 3. Get File Path
            string FilePath = Path.Combine(folderPath, fileName);

            // 4. Save File as Stream [Data Per Time]
            using var fileStream = new FileStream(FilePath, FileMode.Create);
            
            file.CopyTo(fileStream);



            return fileName;

        }

        public static void DeleteFile(string fileName , string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);
            if(File.Exists(filePath))
                File.Delete(filePath);
        }
        
    
    }
}
