using Microsoft.AspNetCore.Mvc;
using AuthWithJWT_Net8.Data;
using AuthWithJWT_Net8.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;

namespace AuthWithJWT_Net8.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (_context.Users.Any(u => u.Email == request.Email))
            return BadRequest("Email already exists");

        var user = new User
        {
            UserId = Guid.NewGuid(),
            Account = RandomNumberGenerator.GetInt32(10000000, 100000000).ToString(),
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Balance = 1000000
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("User registered");
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {

        var user = _context.Users
            .FirstOrDefault(u => u.Email == request.Email);

        if (user == null ||
            !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Unauthorized("Invalid credentials");

        var claims = new[]
        {
            new Claim("userId", user.UserId.ToString()),
            new Claim("account", user.Account.ToString()),
            new Claim("email", user.Email),
            new Claim("balance", user.Balance.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var ExpireMinutes = double.Parse(_configuration["Jwt:ExpireMinutes"]!);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(ExpireMinutes),
            signingCredentials: new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256)
        );

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }

}

public record RegisterRequest(string Email, string Password);
public record LoginRequest(string Email, string Password);