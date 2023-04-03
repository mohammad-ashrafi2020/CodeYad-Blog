using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using CodeYad_Blog.CoreLayer.Utilities;

namespace CodeYad_Blog.CoreLayer.Services.FileManager
{
    public class FileManager : IFileManager
    {


        public void DeleteFile(string fileName, string path)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public string SaveImageAndReturnImageName(IFormFile file, string savePath)
        {
            var isNotImage = !ImageValidation.IsImage(file);
            if (isNotImage)
                throw new Exception();

            return SaveFileAndReturnName(file, savePath);
        }



        public string SaveFileAndReturnName(IFormFile file, string savePath)
        {
            if (file == null)
                throw new Exception("File Is Null");

            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), savePath.Replace("/", "\\"));
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var fullPath = Path.Combine(folderPath, fileName);

            using var stram = new FileStream(fullPath, FileMode.Create);
            file.CopyTo(stram);
            return fileName;
        }
        public async Task<string> SaveFileAndReturnNameAsync(IFormFile file, string savePath)
        {
            if (file == null)
                throw new Exception("File Is Null");

            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), savePath.Replace("/", "\\"));
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var fullPath = Path.Combine(folderPath, fileName);

            await using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);
            return fileName;
        }
    }
}
