using Data.Repositories;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Services;

public class UserService(UserRepository userRepository)
{
	private readonly UserRepository _userRepository = userRepository;
	
	public async Task CreateUserAsync(UserRegistrationForm form)
	{
		var entity = UserFactory.Create(form)!;
		await _userRepository.AddAsync(entity);
	}

	public async Task<IEnumerable<User>> GetUserAsync()
	{
		var entity = await _userRepository.GetAsync();
		return entity.Select(UserFactory.Create)!;
	}
	
	public async Task<User> GetUserByIdAsync(int id)
	{
		var entity = await _userRepository.GetAsync(x => x.Id == id) ?? throw new Exception("Product not found");
		return UserFactory.Create(entity!)!;
	}

	public async Task UpdateUserAsync(UserUpdateForm form)
	{
		var entity = await _userRepository.GetAsync(x => x.Id == form.Id) ?? throw new Exception("Role not found");

		UserFactory.Update(entity, form);

		await _userRepository.UpdateAsync(entity);
	}

	public async Task DeleteUserAsync(int id)
	{
		var entity = await _userRepository.GetAsync(x => x.Id == id) ?? throw new Exception("Product not found");
		await _userRepository.RemoveAsync(entity!);
	}
}