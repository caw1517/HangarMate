using Api.Models;
using Api.Models.Dtos.Teamss;
using Api.Models.Dtos.Users;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
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

        /*GET USER BY ID*/
        /// <param name="userId">The unique identifier of the user</param>
        /// <returns>A basic user profile containing ID, name, and license type</returns>
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
                Team = userProfile.Team != null ? new BasicTeamReturnDto
                {
                    Id = userProfile.Team.Id,
                    TeamName = userProfile.Team.TeamName,
                } : null
            };
        }
        
        
        /*REGISTER NEW USER PROFILE*/
        /// <param name="newUserProfileDto">The user profile data to create</param>
        /// <returns>The newly created user profile</returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BasicUserProfileReturnDto>> RegisterUserProfile(CreateUserProfileDto newUserProfileDto)
        {
            try
            {
                var userId = _permissionService.UserId;

                var newUserProfile = await _usersService.CreateNewUserProfile(newUserProfileDto, userId);

                return CreatedAtAction(nameof(GetUserProfileById), new { userId = newUserProfile.Id }, newUserProfile);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("An error occured while registering a new user");
            }
        }
        
        
        /*UPDATE USER PROFILE*/
        /// <param name="updatedUserProfileDto">The updated information for the profile</param>
        /// <param name="userId">the ID of the user to update, for use by admin only</param>
        /// <returns>The updated user profile</returns>
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<BasicUserProfileReturnDto>> UpdateUserProfile(
            [FromBody] UpdateUserProfileDto updatedUserProfileDto, 
            [FromQuery] Guid? userId)
        {
            UserProfile updatedProfile;

            try
            {
                
                //We are admin
                if (userId != null)
                {
                    //If userId was sent, it must be an admin doing it. Otherwise, we will just use the userId from the request.
                    if (_permissionService.SiteRole != SiteRole.Admin)
                    {
                        return Forbid();
                    }
                    updatedProfile = await _usersService.UpdateUserProfile(updatedUserProfileDto, userId.Value);
                }
                else
                {
                    updatedProfile =  await _usersService.UpdateUserProfile(updatedUserProfileDto, _permissionService.UserId);
                }

                var returnProfile = new BasicUserProfileReturnDto
                {
                    Id = updatedProfile.Id,
                    FirstName = updatedProfile.FirstName,
                    LastName = updatedProfile.LastName,
                    LicenseType = updatedProfile.LicenseType,
                };
                
                return Ok(returnProfile);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
