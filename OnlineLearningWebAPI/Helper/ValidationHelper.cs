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
    }
}
