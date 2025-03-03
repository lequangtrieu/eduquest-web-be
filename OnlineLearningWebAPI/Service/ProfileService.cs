using Microsoft.EntityFrameworkCore;
using OnlineLearningWebAPI.DTOs.request.ProfileRequest;
using OnlineLearningWebAPI.DTOs.response;
using OnlineLearningWebAPI.Models;
using OnlineLearningWebAPI.Repository.IRepository;
using OnlineLearningWebAPI.Service.IService;

namespace OnlineLearningWebAPI.Service
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly IRepository<Profile> _profileRepository;

        public ProfileService(IRepository<Account> accountRepository,
            IRepository<Profile> profileRepository)
        {
            _accountRepository = accountRepository;
            _profileRepository = profileRepository;
        }

        public async Task<GeneralResponse> AddNewProfile(AddProfileRequest request)
        {

            return new GeneralResponse(true, "Profile added");
        }

        public async Task<GeneralResponse> UpdateProfile(UpdateProfileRequest request)
        {
            Profile profile = await _profileRepository.GetByIdAsync(request.ProfileId);

            if (profile == null)
            {
                return new GeneralResponse(false, "Can not find profile");
            }

            if (request.Address != null) profile.Address = request.Address;
            if (request.Address != null) profile.Avatar = request.Avatar;
            if (request.Address != null) profile.IsVIP = request.IsVIP;
            if (request.Address != null) profile.UserName = request.UserName;

            try
            {
                _profileRepository.Update(profile);
                await _profileRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new GeneralResponse(false, ex.Message);
            }

            return new GeneralResponse(true, "Profile updated");
        }

        public Profile GetProfileByAccountId(string id)
        {
            IEnumerable<Profile> allProfiles = _profileRepository.GetQuery();

            IEnumerable<Profile> profiles = allProfiles.Where(x => x.AccountId == id);

            if (profiles.Count() == 0)
            {
                return new Profile();
            }
            return profiles.First();
        }
    }
}
