using Data.Entities;
using Data.Repositories;
using Domain.Extensions;
using Domain.Models;
using Domain.Dtos;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace Infrastructure.Services;

public interface IUserService
{
  Task<UserResult> AddUserToRoleAsync(string userId, string roleName);
  Task<UserResult> CreateUserAsync(SignUpFormData formData, string roleName = "User");
  Task<UserResult> CreateUserExtendedAsync(AddMemberFormData formData, string roleName = "User");
  Task<UserResult<IEnumerable<User>>> GetAllUsersAsync();
}

public class UserService(IUserRepository userRepository, UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager) : IUserService
{
  private readonly IUserRepository _userRepository = userRepository;
  private readonly UserManager<UserEntity> _userManager = userManager;
  private readonly RoleManager<IdentityRole> _roleManager = roleManager;

  public async Task<UserResult> CreateUserAsync(SignUpFormData formData, string roleName = "User")
  {
    if (formData == null)
    {
      return new UserResult { Succeeded = false, StatusCode = 400, Error = "Not all required fields have a valid input." };
    }

    var existsResult = await _userRepository.ExistsAsync(x => x.Email == formData.Email);
    if (existsResult.Succeeded)
    {
      return new UserResult { Succeeded = false, StatusCode = 409, Error = "User with the same email already exists." };
    }

    try
    {
      var userEntity = formData.MapTo<UserEntity>();

      userEntity.UserName = formData.Email;
      userEntity.Email = formData.Email;
      userEntity.NormalizedUserName = formData.Email?.ToUpperInvariant();
      userEntity.NormalizedEmail = formData.Email?.ToUpperInvariant();

      if (string.IsNullOrEmpty(userEntity.Image))
      {
        userEntity.Image = "/images/profile-picture-placeholder.svg";
      }

      var password = $"A{Guid.NewGuid():N}a!"; // Temp strong password
      var result = await _userManager.CreateAsync(userEntity, password);
      if (result.Succeeded)
      {
        var addToRoleResult = await AddUserToRoleAsync(userEntity.Id, roleName);
        return result.Succeeded
          ? new UserResult { Succeeded = true, StatusCode = 201 }
          : new UserResult { Succeeded = false, StatusCode = 201, Error = "User was created but not added to role." };
      }

      return new UserResult { Succeeded = false, StatusCode = 500, Error = "Unable to create user." };
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex.Message);
      return new UserResult { Succeeded = false, StatusCode = 500, Error = ex.Message };
    }
  }

  public async Task<UserResult> CreateUserExtendedAsync(AddMemberFormData formData, string roleName = "User")
  {
    if (formData == null)
    {
      return new UserResult { Succeeded = false, StatusCode = 400, Error = "Not all required fields have a valid input." };
    }

    var existsResult = await _userRepository.ExistsAsync(x => x.Email == formData.Email);
    if (existsResult.Succeeded)
    {
      return new UserResult { Succeeded = false, StatusCode = 409, Error = "User with the same email already exists." };
    }

    try
    {
      var userEntity = formData.MapTo<UserEntity>();

      userEntity.UserName = formData.Email;
      userEntity.Email = formData.Email;
      userEntity.NormalizedUserName = formData.Email?.ToUpperInvariant();
      userEntity.NormalizedEmail = formData.Email?.ToUpperInvariant();

      if (string.IsNullOrEmpty(userEntity.Image))
      {
        userEntity.Image = "/images/profile-picture-placeholder.svg";
      }

      var password = $"A{Guid.NewGuid():N}a!"; // Temp strong password
      var result = await _userManager.CreateAsync(userEntity, password);
      if (result.Succeeded)
      {
        var addToRoleResult = await AddUserToRoleAsync(userEntity.Id, roleName);
        return result.Succeeded
          ? new UserResult { Succeeded = true, StatusCode = 201 }
          : new UserResult { Succeeded = false, StatusCode = 201, Error = "User was created but not added to role." };
      }

      return new UserResult { Succeeded = false, StatusCode = 500, Error = "Unable to create user." };
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex.Message);
      return new UserResult { Succeeded = false, StatusCode = 500, Error = ex.Message };
    }
  }

  public async Task<UserResult<IEnumerable<User>>> GetAllUsersAsync()
  {
    var result = await _userRepository.GetAllAsync();
    return new UserResult<IEnumerable<User>> { Succeeded = true, StatusCode = 200, Result = result.Result };
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