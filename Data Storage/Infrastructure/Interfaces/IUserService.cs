using Infrastructure.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Interfaces;

public interface IUserService
{
	Task CreateUserAsync(UserRegistrationForm form);
	Task<IEnumerable<User>> GetUsersAsync();
	Task<User> GetUserByIdAsync(int id);
	Task UpdateUserAsync(UserUpdateForm form);
	Task DeleteUserAsync(int id);
}