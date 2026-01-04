using System.ComponentModel.DataAnnotations;
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

    public async Task<UserProfile> CreateNewUserProfile(CreateUserProfileDto newUser, Guid userId)
    {
        if(_context.UserProfiles.Any(x => x.Id == userId))
            throw new ArgumentException("There is already a profile for this user.");
        
        var newUserProfile = new UserProfile
        {
            Id = userId,
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

    public async Task<UserProfile> UpdateUserProfile(UpdateUserProfileDto updateUser, Guid userId)
    {
        var userProfile = await _context.UserProfiles.FindAsync(userId);
        
        if(userProfile == null)
            throw new ArgumentException("There is no profile for this user.");
        
        if (updateUser.FirstName != null)
            userProfile.FirstName = updateUser.FirstName;
        
        if(updateUser.LastName != null)
            userProfile.LastName = updateUser.LastName;
        
        if (updateUser.LicenseType != null)
            userProfile.LicenseType = (LicenseType)updateUser.LicenseType;
        
        userProfile.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        
        return userProfile;
    }

}