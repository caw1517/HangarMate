using Api.Context;
using Api.Models;
using Api.Models.Dtos.Users;

namespace Api.Services;

public class UsersService
{
    private readonly DatabaseContext _context;

    public UsersService(DatabaseContext context)
    {
        _context = context; 
    }

    public async Task<UserProfile?> GetUserProfileById(Guid id)
    {
        return await _context.UserProfiles.FindAsync(id);
    }

    public async Task<UserProfile> CreateNewUserProfile(CreateUserProfileDto newUser)
    {
        var newUserProfile = new UserProfile
        {
            Id = newUser.Id,
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            SiteRole = SiteRole.Guest,
            LicenseType = newUser.LicenseType,
        };
        
        var userProfile = _context.UserProfiles.Add(newUserProfile);
        await _context.SaveChangesAsync();

        return newUserProfile;
    }

}