using CloudinaryDotNet.Actions;

namespace DatingApi.Services.PhotoService
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> UploadPhotoAsync(IFormFile file);
        Task<DeletionResult>DeletePhotoAsync(string publicId);
    }
}
