using System;
using System.IO;
using System.Text;

namespace MyProjectForJuly2020.Helpers
{
    public class MyTools
    {
        public static string FullPathFolderImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh");

        public static string NoImage = "no-image-available.png";
        public static string ImageToBase64(string fileName, string folder)
        {
            var fullPath = Path.Combine(FullPathFolderImage, folder, fileName);
            if (File.Exists(fullPath))
            {
                byte[] data = File.ReadAllBytes(fullPath);
                return Convert.ToBase64String(data);
            }

            return null;
        }

        public static string GetRandom(int length = 5)
        {
            var pattern = @"1234567890qazwsxedcrfvtgbyhn@#$%";
            var rd = new Random();
            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
                sb.Append(pattern[rd.Next(0, pattern.Length)]);

            return sb.ToString();
        }
    }
}
