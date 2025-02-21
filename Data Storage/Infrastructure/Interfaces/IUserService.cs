using Infrastructure.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Interfaces;

public interface IUserService
{
	Task<bool> CreateUserAsync(UserRegistrationForm form);
	Task<IEnumerable<User?>> GetUsersAsync();
	Task<User?> GetUserByIdAsync(int id);
	Task<bool> UpdateUserAsync(UserUpdateForm form);
	Task<bool> DeleteUserAsync(int id);
}