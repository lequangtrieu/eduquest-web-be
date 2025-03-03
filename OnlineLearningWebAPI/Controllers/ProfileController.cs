using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningWebAPI.DTOs;
using OnlineLearningWebAPI.DTOs.request.ProfileRequest;
using OnlineLearningWebAPI.DTOs.response;
using OnlineLearningWebAPI.Models;
using OnlineLearningWebAPI.Service.IService;

namespace OnlineLearningWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        // GET: api/teachers/{id}
        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetProfileByAccountId(string accountId)
        {
            Profile profile = _profileService.GetProfileByAccountId(accountId);

            if (profile == null) return NotFound(new { message = "[ProfileController] | GetProfileByAccountId | Profile not found" });

            return Ok(profile);
        }

    }
}
