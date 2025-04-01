namespace OnlineLearningWebAPI.Service.IService
{
    public interface IFirebaseService
    {
        Task<string> UploadImage(IFormFile file);
        Task<string> UploadVideo(IFormFile file);
    }
}
