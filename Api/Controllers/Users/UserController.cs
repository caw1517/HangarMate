using Api.Models.Dtos.Users;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly UsersService _usersService;
        private readonly IPermissionService _permissionService;

        public UserController(UsersService usersService, IPermissionService permissionService)
        {
            _usersService = usersService;
            _permissionService = permissionService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<BasicUserProfileReturnDto>> GetUserProfileById(Guid userId)
        {
            var userProfile = await _usersService.GetUserProfileById(userId);
            
            if(userProfile == null)
                return NotFound("User not found");

            return new BasicUserProfileReturnDto
            {
                Id = userProfile.Id,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                LicenseType = userProfile.LicenseType,
            };
        }
        
        [HttpPost]
        public async Task<ActionResult<BasicUserProfileReturnDto>> RegisterUserProfile(CreateUserProfileDto newUserProfileDto)
        {
            try
            {
                var newUserProfile = await _usersService.CreateNewUserProfile(newUserProfileDto);

                return CreatedAtAction(nameof(GetUserProfileById), new { userId = newUserProfile.Id }, newUserProfile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}
