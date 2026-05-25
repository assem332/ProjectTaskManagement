using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Application.DTOs;
using ProjectTaskManagement.Application.Interfaces;
using ProjectTaskManagement.Domain.Entities;
using ProjectTaskManagement.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace ProjectTaskManagement.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthService(ApplicationDbContext context , IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;

        }

        public async Task<string> RegisterAsync(RegisterDto model)
        {
            var userExists = await _context.Users.AnyAsync(x => x.Email == model.Email);
            if(userExists)
              return "Email already exists.";

            var user = new ApplicationUser
            {
                Name = model.Name,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return "User registered successfully";
        }
        public async Task<string> LoginAsync(LoginDto model)
        {
            var user = await _context.Users
                 .FirstOrDefaultAsync(
                      x => x.Email == model.Email);

            if (user == null)
                return "Invalid email or password";

            bool isPasswordValid =
                 BCrypt.Net.BCrypt.Verify(
                  model.Password,
                  user.PasswordHash);

            if (!isPasswordValid)
                return "Invalid email or password";

            var token = _jwtService.GenerateToken(user);

            return token;
        }

    }

    }

