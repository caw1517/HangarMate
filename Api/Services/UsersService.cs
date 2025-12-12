using Api.Context;

namespace Api.Services;

public class UsersService
{
    private readonly DatabaseContext _context;

    public UsersService(DatabaseContext context)
    {
        _context = context; 
    }
    
    

}