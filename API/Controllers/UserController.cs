using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly DataContext _context;

    public UserController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]

    public async Task<ActionResult<IEnumerable<AppUser>>> Users()
    {
        var users=await _context.Users.ToListAsync();
        return users;
    }

    [HttpGet("{id}")]

    public  async Task<ActionResult<AppUser>> GetUser(int id)
    {
        var user=await _context.Users.FindAsync(id);
        return user;
    }
}


