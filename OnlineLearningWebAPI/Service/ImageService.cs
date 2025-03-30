using Firebase.Auth;
using Firebase.Storage;
using Microsoft.Extensions.Options;
using OnlineLearningWebAPI.Configurations;

namespace OnlineLearningWebAPI.Service.IService
{
    public class ImageService : IImageService
    {
        private readonly IOptions<FirebaseConfigure> _firebaseConfig;
        public ImageService(
            IOptions<FirebaseConfigure> firebaseConfig
            )
        {
            _firebaseConfig = firebaseConfig;
        }
        public async Task<string> UploadImage(IFormFile file)
        {
            var apiKey = _firebaseConfig.Value.ApiKey;
            var bucket = _firebaseConfig.Value.Bucket;
            var email = _firebaseConfig.Value.Email;
            var password = _firebaseConfig.Value.Password;
            var fileName = Path.GetFileName(file.FileName);
            using var stream = new FileStream(Path.Combine(Path.GetTempPath(), fileName), FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Position = 0;

            var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(email, password);

            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child("images")
                .Child(DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString())
                .PutAsync(stream, cancellation.Token);
            try
            {
                string link = await task;
                return link;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
