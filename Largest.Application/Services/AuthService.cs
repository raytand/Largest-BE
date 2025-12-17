using Largest.Application.Interfaces;
using Largest.Application.DTOs;
using Largest.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Largest.Application.Services
{

    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _jwtKey;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;

        public AuthService(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _jwtKey = config["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key not found");
            _jwtIssuer = config["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer not found");
            _jwtAudience = config["Jwt:Audience"] ?? throw new ArgumentNullException("Jwt:Audience not found");
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null) return null;

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            var claims = new[]
            {
            new Claim("id", user.Id.ToString()),
            new Claim("email", user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtIssuer,
                audience: _jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ServiceResponseDto> RegisterAsync(UserRegisterDto request)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
            if (existingUser != null){return new ServiceResponseDto { Success = false, Message = "User already exists." };}
            if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 6) { return new ServiceResponseDto { Success = false, Message = "Password too short." }; }
            if (!request.Email.Contains("@")) { return new ServiceResponseDto { Success = false, Message = "Invalid email." }; }    
            
            var user = new User
            {
                Username = request.Username,
                PasswordHash = HashPassword(request.Password),
                Email = request.Email
            };

            await _userRepository.AddUserAsync(user);

            return new ServiceResponseDto { Success = true, Message = "User registered successfully." };
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}