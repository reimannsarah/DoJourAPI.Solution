using DoJourAPI.Models;
using DoJourAPI.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DoJourAPI.Services;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;

  public UserService(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<User> GetUserByIdAsync(Guid id)
  {
    return await _userRepository.GetByIdAsync(id);
  }

  public async Task<User> GetUserByEmailAsync(string email)
  {
    try
    {
      var user = await _userRepository.GetByEmailAsync(email);
      return user;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
      throw;
    }
  }

  public async Task CreateUserAsync(User user)
{
    try
    {
        await _userRepository.CreateAsync(user);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        throw;
    }
}

  public async Task UpdateUserAsync(User user)
  {
    await _userRepository.UpdateAsync(user);
  }

  public async Task DeleteUserAsync(Guid id)
  {
    await _userRepository.DeleteAsync(id);
  }

  public string GenerateToken(User user)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET_KEY"));
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(new Claim[] 
      {
        new Claim(ClaimTypes.Name, user.UserId.ToString(), ClaimValueTypes.String),
        new Claim(ClaimTypes.Email, user.Email)
        // Add other claims as needed
      }),
      Expires = DateTime.UtcNow.AddDays(7), // Token expiration, adjust as needed
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }
}