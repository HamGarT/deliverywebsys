namespace deliveryapp.Services
{
    public interface IImageService
    {
        bool DeleteImage(string imageUrl);
        Task<string> SaveImageAsync(IFormFile image, string folder);
    }
}
