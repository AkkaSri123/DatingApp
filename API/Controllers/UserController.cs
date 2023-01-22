using API.Controllers;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class UserController : BaseApiController
{
    private readonly DataContext _context;

    public UserController(DataContext context)
    {
        _context = context;
    }
    [AllowAnonymous]
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


