namespace LowPolyBacklogApi.Services.Interfaces
{
    public interface IImageService
    {
        Task<string?> UploadImageAsync(IFormFile file);
    }
}
