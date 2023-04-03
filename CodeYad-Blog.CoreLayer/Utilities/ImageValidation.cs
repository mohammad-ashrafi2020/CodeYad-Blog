using System;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace CodeYad_Blog.CoreLayer.Utilities
{
    public class ImageValidation
    {
        public static bool IsImage(string imageName)
        {
            var extension = Path.GetExtension(imageName);
            if (extension == null)
                return false;

            return extension.ToLower() == ".png" || extension.ToLower() == ".jpg";
        }

        public static bool IsImage(IFormFile file)
        {
            try
            {
                using var image = Image.FromStream(file.OpenReadStream());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}