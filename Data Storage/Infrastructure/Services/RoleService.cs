using Data.Repositories;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Dtos;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class RoleService(RoleRepository roleRepository) : IRoleService
{
	private readonly RoleRepository _roleRepository = roleRepository;

	public async Task<bool> CreateRoleAsync(RoleRegistrationForm form)
	{
		var entity = RoleFactory.Create(form);
		if (entity == null)
		{
			return false;
		}

		return await _roleRepository.ExecuteInTransactionAsync(async () =>
		{
			await _roleRepository.AddAsync(entity);
		});
	}

	public async Task<IEnumerable<Role?>> GetRolesAsync()
	{
		var entities = await _roleRepository.GetAsync();
		return entities?.Select(RoleFactory.Create)!;
	}

	public async Task<Role?> GetRoleByIdAsync(int id)
	{
		var entity = await _roleRepository.GetAsync(x => x.Id == id);
		if (entity == null)
		{
			return null;
		}

		return RoleFactory.Create(entity);
	}

	public async Task<bool> UpdateRoleAsync(RoleUpdateForm form)
	{
		var entity = await _roleRepository.GetAsync(x => x.Id == form.Id);
		if (entity == null)
		{
			return false;
		}

		RoleFactory.Update(entity, form);

		return await _roleRepository.ExecuteInTransactionAsync(async () =>
		{
			await _roleRepository.UpdateAsync(entity);
		});
	}

	public async Task<bool> DeleteRoleAsync(int id)
	{
		var entity = await _roleRepository.GetAsync(x => x.Id == id);
		if (entity == null)
		{
			return false;
		}

		return await _roleRepository.ExecuteInTransactionAsync(async () =>
		{
			await _roleRepository.RemoveAsync(entity);
		});
	}
}
