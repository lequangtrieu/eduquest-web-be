namespace OnlineLearningWebAPI.Service.IService
{
    public interface IImageService
    {
        Task<string> UploadImage(IFormFile file);
    }
}
