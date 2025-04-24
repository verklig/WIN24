using Infrastructure.Models;
using Data.Repositories;
using Data.Entities;
using Domain.Extensions;
using Microsoft.AspNetCore.Identity;
using Domain.Models;

namespace Infrastructure.Services;

public interface IUserService
{
  Task<UserResult> AddUserToRoleAsync(string userId, string roleName);
  Task<UserResult> GetUserResultAsync();
}

public class UserService(IUserRepository userRepository, UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager) : IUserService
{
  private readonly IUserRepository _userRepository = userRepository;
  private readonly UserManager<UserEntity> _userManager = userManager;
  private readonly RoleManager<IdentityRole> _roleManager = roleManager;

  public async Task<UserResult> GetUserResultAsync()
  {
    var result = await _userRepository.GetAllAsync();
    return result.MapTo<UserResult>();
  }

  public async Task<UserResult> AddUserToRoleAsync(string userId, string roleName)
  {
    if (!await _roleManager.RoleExistsAsync(roleName))
    {
      return new UserResult { Succeeded = false, StatusCode = 404, Error = "Role does not exist." };
    }

    var user = await _userManager.FindByIdAsync(userId);
    if (user == null)
    {
      return new UserResult { Succeeded = false, StatusCode = 404, Error = "Role does not exist." };
    }

    var result = await _userManager.AddToRoleAsync(user, roleName);
    return result.Succeeded
      ? new UserResult { Succeeded = true, StatusCode = 200 }
      : new UserResult { Succeeded = false, StatusCode = 500, Error = "Unable to add user to role." };
  }
}