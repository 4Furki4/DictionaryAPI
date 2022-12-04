using DictionaryAPI.Context;
using DictionaryAPI.DTOs;
using DictionaryAPI.Models.Concretes;
using DictionaryAPI.Services.User_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DictionaryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DictionaryAuthController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly DictionaryDB context;
        private readonly IUserService userService;

        public DictionaryAuthController(IConfiguration configuration, DictionaryDB context, IUserService userService)
        {
            this.configuration = configuration;
            this.context = context;
            this.userService = userService;
        }
        [HttpPost("/adminregister")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDTO>> AddAdmin(UserDTO request)
        {
            EncodePassword(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User user = new User
            {
                Name = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                roleId = 1
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return Ok(request);
        }

        [HttpPost("/register")]
        public async Task<ActionResult<UserDTO>> UserRegister(UserDTO request)
        {
            EncodePassword(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User user = new User
            {
                Name = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                roleId = 2
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return Ok(request);
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDTO request)
        {
            var user = await context.Users.SingleAsync(user => user.Name == request.Username);
            if (user is null)
                return BadRequest("User not found");

            if (!VerifyUser(request.Password, user.PasswordHash, user.PasswordSalt))
                return BadRequest("Wrong password!");
            string token = CreateToken(user, user.roleId);
            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken, user);
            await context.SaveChangesAsync();
            return Ok(token);
        }
        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<ActionResult<string>> RefreshToken(UserDTO userdto)
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var user = await context.Users.FirstOrDefaultAsync(user => user.Name == userdto.Username);
            if (user is null)
                return BadRequest("User not found");
            if (!user.RefreshToken.Token.Equals(refreshToken))
                return Unauthorized("Invalid refresh token");
            if (user.RefreshToken.Expires < DateTime.Now)
                return Unauthorized("Refresh token has expired");

            string token = CreateToken(user, user.roleId);

            var newRefreshToken = GenerateRefreshToken();

            SetRefreshToken(newRefreshToken, user); 
            await context.SaveChangesAsync();
            return Ok(token);
        }

        [HttpGet]
        [Authorize] 
        public ActionResult<string> GetName()
        {
            return Ok(userService.GetMe());
        }

        private string CreateToken(User user, int roleId)
        {
            List<Claim> claims = new List<Claim>();
            if (roleId == (int)Roles.Admin)
            {
                claims.Add(new Claim(ClaimTypes.Name, user.Name));
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                //new Claim(ClaimTypes.NameIdentifier, user.Name),
                //    new Claim(ClaimTypes.Role, "Admin")
            }
            else if(roleId == (int)Roles.User)
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Name));
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:TokenSecurityKey").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        void EncodePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        bool VerifyUser(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);

            }
        }

        RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddHours(1),
                TokenCreated = DateTime.Now
            };
            return refreshToken;
        }

        void SetRefreshToken(RefreshToken newRefreshToken, User user)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken;
        }

        enum Roles
        {
            Admin = 1,
            User = 2
        }
    }
}
