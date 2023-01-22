using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>>Register(RegisterDTO registerdto)
        {
            if(await UserExists(registerdto.UserName)) return BadRequest("username is taken");
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerdto.UserName.ToLower(),
                HashPassword= hmac.ComputeHash(Encoding.UTF8.GetBytes(registerdto.Password)),
                HashSalt= hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO logindto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == logindto.UserName);
            if (user==null) return Unauthorized("Username is invalid");
            using var hmac=new HMACSHA512(user.HashSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(logindto.Password));
            for(int i=0;i<computedHash.Length;i++)
            {
                if(computedHash[i]!=user.HashPassword[i])
                return Unauthorized("Invalid password");
            }
            return new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool>UserExists(string username)
        {
            return await _context.Users.AnyAsync(x=>x.UserName==username.ToLower());
        }
    }
}