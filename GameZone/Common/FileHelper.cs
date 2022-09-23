using System.Drawing;

namespace GameZone.Common
{
    public static class FileHelper
    {
        public static string FullPathToMedaiFolder(string folderInMedia)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Media", folderInMedia);
        }

        public static void CreateDirectory(string fullPath)
        {
            if (fullPath == null)
                throw new ArgumentNullException(nameof(fullPath));

            DirectoryInfo di;
            if (!Directory.Exists(fullPath))
                di = Directory.CreateDirectory(fullPath);
        }

        public static async Task UploadFile(IFormFile file, string imagePath)
        {
            if (imagePath == null)
                throw new ArgumentNullException(nameof(imagePath));

            if (file is not null)
            {
                using (var stream = File.Create(imagePath))
                {
                    await file.CopyToAsync(stream);
                }
            }
        }

        public static void ResizeImage(IFormFile file, string imagePath, int newWidth, int newHeight = 0)
        {
            if (!file.ContentType.Contains("image"))
                throw new ArgumentException("The file is not an image!");
            if (newWidth <= 0)
                throw new ArgumentException("Width cannot be less than 0!");

            Image image = Image.FromStream(file.OpenReadStream(), true, true);
            if (newHeight == 0)
            {
                var multiplier = (float) newWidth / image.Width;
                newHeight = (int)(multiplier * image.Height);
            }

            Bitmap resized = new Bitmap(image, newWidth, newHeight);
            resized.Save(imagePath);
        }
    }
}
