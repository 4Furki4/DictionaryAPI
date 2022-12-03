﻿using DictionaryAPI.Context;
using DictionaryAPI.DTOs;
using DictionaryAPI.Models.Concretes;
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

        public DictionaryAuthController(IConfiguration configuration, DictionaryDB context)
        {
            this.configuration = configuration;
            this.context = context;
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

        [HttpPost("/userregister")]
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
            return Ok(token);
        }

        private string CreateToken(User user, int roleId)
        {
            List<Claim> claims = new List<Claim>();
            if (roleId == (int)Roles.Admin)
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Name));
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
        enum Roles
        {
            Admin = 1,
            User = 2
        }
    }
}