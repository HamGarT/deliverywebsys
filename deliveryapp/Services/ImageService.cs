using Microsoft.AspNetCore.Hosting;

namespace deliveryapp.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _env;

        public ImageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveImageAsync(IFormFile image, string folder)
        {
            string imagePath = string.Empty;

            if (image != null && image.Length > 0)
            {

                if (image.Length > 5 * 1024 * 1024)
                    throw new InvalidOperationException("El archivo es demasiado grande (máximo 5MB)");

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var fileExtension = Path.GetExtension(image.FileName).ToLowerInvariant();

                if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
                    throw new InvalidOperationException("Formato de imagen no válido. Use: jpg, jpeg, png, gif, webp");

                var fileName = $"{Guid.NewGuid()}{fileExtension}";


                var uploadsFolder = Path.Combine(_env.WebRootPath, "images", folder);


                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }


                imagePath = $"/images/{folder}/{fileName}";
            }
            return imagePath;
        }

        public bool DeleteImage(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl)) return false;

            try
            {
                var relativePath = imageUrl.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString());
                var fullPath = Path.Combine(_env.WebRootPath, relativePath);
                var normalizedWebRoot = Path.GetFullPath(_env.WebRootPath);
                var normalizedFullPath = Path.GetFullPath(fullPath);

                if (!normalizedFullPath.StartsWith(normalizedWebRoot + Path.DirectorySeparatorChar)
                    && !normalizedFullPath.Equals(normalizedWebRoot))
                {
                    return false; 
                }

                if (File.Exists(normalizedFullPath))
                {
                    File.Delete(normalizedFullPath);
                    return true;
                }

                return false; 
            }
            catch (Exception)
            {
                return false; 
            }
        }
    }
}
