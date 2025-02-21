using Data.Repositories;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Dtos;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class UserService(UserRepository userRepository) : IUserService
{
	private readonly UserRepository _userRepository = userRepository;

	public async Task<bool> CreateUserAsync(UserRegistrationForm form)
	{
		var entity = UserFactory.Create(form);
		if (entity == null)
		{
			return false;
		}

		return await _userRepository.AddAsync(entity);
	}

	public async Task<IEnumerable<User?>> GetUsersAsync()
	{
		var entities = await _userRepository.GetAsync();
		return entities?.Select(UserFactory.Create)!;
	}

	public async Task<User?> GetUserByIdAsync(int id)
	{
		var entity = await _userRepository.GetAsync(x => x.Id == id);
		if (entity == null)
		{
			return null;
		}

		return UserFactory.Create(entity);
	}

	public async Task<bool> UpdateUserAsync(UserUpdateForm form)
	{
		var entity = await _userRepository.GetAsync(x => x.Id == form.Id);
		if (entity == null)
		{
			return false;
		}

		UserFactory.Update(entity, form);
		return await _userRepository.UpdateAsync(entity);
	}

	public async Task<bool> DeleteUserAsync(int id)
	{
		var entity = await _userRepository.GetAsync(x => x.Id == id);
		if (entity == null)
		{
			return false;
		}

		return await _userRepository.RemoveAsync(entity!);
	}
}