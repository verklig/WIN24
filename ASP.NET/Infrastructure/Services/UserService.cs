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
  Task<UserResult> CreateUserByFormAsync(AddMemberFormData formData, string roleName = "User");
  Task<UserResult> CreateUserBySignUpAsync(SignUpFormData formData, string roleName = "User");
  Task<UserResult<IEnumerable<User>>> GetAllUsersAsync();
  Task<UserResult<User>> GetUserAsync(string userId);
  Task<UserResult> RemoveUserAsync(string userId);
  Task<UserResult> UpdateUserAsync(EditMemberFormData formData);
}

public class UserService(IUserRepository userRepository, UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager) : IUserService
{
  private readonly IUserRepository _userRepository = userRepository;
  private readonly UserManager<UserEntity> _userManager = userManager;
  private readonly RoleManager<IdentityRole> _roleManager = roleManager;

  public async Task<UserResult> CreateUserBySignUpAsync(SignUpFormData formData, string roleName = "User")
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

      userEntity.Email = formData.Email;
      userEntity.UserName = formData.Email;
      userEntity.NormalizedUserName = formData.Email?.ToUpperInvariant();
      userEntity.NormalizedEmail = formData.Email?.ToUpperInvariant();

      if (string.IsNullOrEmpty(userEntity.Image))
      {
        userEntity.Image = "/images/profile-picture-placeholder.svg";
      }

      var result = await _userManager.CreateAsync(userEntity);
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

  public async Task<UserResult> CreateUserByFormAsync(AddMemberFormData formData, string roleName = "User")
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

      userEntity.Email = formData.Email;
      userEntity.UserName = formData.Email;
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

  public async Task<UserResult> UpdateUserAsync(EditMemberFormData formData)
  {
    if (formData == null)
    {
      return new UserResult { Succeeded = false, StatusCode = 400, Error = "Not all required fields have a valid input." };
    }

    var userResult = await _userRepository.GetAsync(x => x.Id == formData.Id);
    if (!userResult.Succeeded || userResult.Result == null)
    {
      return new UserResult { Succeeded = false, StatusCode = 404, Error = "User not found." };
    }

    try
    {
      // Not using MapTo here to not mess up Identity
      var existingUser = await _userManager.FindByIdAsync(formData.Id);

      existingUser!.FirstName = formData.FirstName;
      existingUser.LastName = formData.LastName;
      existingUser.Email = formData.Email;
      existingUser.PhoneNumber = formData.PhoneNumber;
      existingUser.JobTitle = formData.JobTitle;
      existingUser.Address = formData.Address;

      if (!string.IsNullOrWhiteSpace(formData.DateOfBirth))
      {
        existingUser.DateOfBirth = formData.DateOfBirth;
      }

      existingUser.UserName = formData.Email;
      existingUser.NormalizedUserName = formData.Email?.ToUpperInvariant();
      existingUser.NormalizedEmail = formData.Email?.ToUpperInvariant();

      if (string.IsNullOrEmpty(existingUser.Image))
      {
        existingUser.Image = "/images/profile-picture-placeholder.svg";
      }

      var result = await _userManager.UpdateAsync(existingUser);

      return result.Succeeded
        ? new UserResult { Succeeded = true, StatusCode = 200 }
        : new UserResult { Succeeded = false, StatusCode = 500, Error = "Unable to update user." };
    }
    catch (Exception ex)
    {
      Debug.WriteLine(ex.Message);
      return new UserResult { Succeeded = false, StatusCode = 500, Error = ex.Message };
    }
  }

  public async Task<UserResult> RemoveUserAsync(string userId)
  {
    if (string.IsNullOrEmpty(userId))
    {
      return new UserResult { Succeeded = false, StatusCode = 400, Error = "User ID cannot be null or empty." };
    }

    try
    {
      var user = await _userManager.FindByIdAsync(userId);

      if (user == null)
      {
        return new UserResult { Succeeded = false, StatusCode = 404, Error = "User not found." };
      }

      var result = await _userManager.DeleteAsync(user);
      return result.Succeeded
        ? new UserResult { Succeeded = true, StatusCode = 200 }
        : new UserResult { Succeeded = false, StatusCode = 500, Error = "Unable to delete user." };
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

    if (!result.Succeeded || result.Result == null)
    {
      return new UserResult<IEnumerable<User>> { Succeeded = false, StatusCode = 404, Error = "No users found." };
    }

    var users = result.Result.Select(userEntity => new User
    {
      Id = userEntity.Id,
      FirstName = userEntity.FirstName,
      LastName = userEntity.LastName,
      Email = userEntity.Email,
      PhoneNumber = userEntity.PhoneNumber,
      JobTitle = userEntity.JobTitle,
      Address = userEntity.Address,
      Image = userEntity.Image
    });

    return new UserResult<IEnumerable<User>> { Succeeded = true, StatusCode = 200, Result = users };
  }

  public async Task<UserResult<User>> GetUserAsync(string userId)
  {
    if (string.IsNullOrEmpty(userId))
    {
      return new UserResult<User> { Succeeded = false, StatusCode = 400, Error = "User ID cannot be null or empty." };
    }

    var userEntity = await _userManager.FindByIdAsync(userId);
    if (userEntity == null)
    {
      return new UserResult<User> { Succeeded = false, StatusCode = 404, Error = "User not found." };
    }

    var user = userEntity.MapTo<User>();
    return new UserResult<User> { Succeeded = true, StatusCode = 200, Result = user };
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