namespace OnlineLearningWebAPI.Helper
{
    public class ValidationHelper
    {
        public static bool IsValidImage(IFormFile file)
        {
            var validTypes = new[] { "image/jpg", "image/png", "image/jpeg" };

            if (file == null && file.Length == 0)
            {
                return false;
            }
            return validTypes.Contains(file.ContentType);
        }

        public static bool IsValidVideo(IFormFile file)
        {
            var validTypes = new[] { "video/mp4"};

            if (file == null && file.Length == 0)
            {
                return false;
            }
            return validTypes.Contains(file.ContentType);
        }
    }
}
