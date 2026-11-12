using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Interfaces;


namespace InventoryManagement.Application.Services;

public class AuthService : IAuthService
{
    private readonly ITokenRevocationService _tokenRevocationService;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public AuthService(
     IUserRepository userRepository,
     ITokenService tokenService,
     ITokenRevocationService tokenRevocationService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _tokenRevocationService = tokenRevocationService;
    }

    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        var emailExists = await _userRepository.EmailExistsAsync(dto.Email);

        if (emailExists)
        {
            return "Email already exists.";
        }

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        await _userRepository.AddAsync(user);

        return "Registration successful.";
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);

        if (user == null)
        {
            return "Invalid email or password.";
        }

        var passwordValid = BCrypt.Net.BCrypt.Verify(
            dto.Password,
            user.PasswordHash);

        if (!passwordValid)
        {
            return "Invalid email or password.";
        }

        var token = _tokenService.GenerateToken(
            user.Id,
            user.Email);

        return token;
    }

    public Task<bool> LogoutAsync(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return Task.FromResult(false);
        }

        _tokenRevocationService.RevokeToken(token);

        return Task.FromResult(true);
    }
}
