using Data.Repositories;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Services;

public class RoleService(RoleRepository roleRepository)
{
	private readonly RoleRepository _roleRepository = roleRepository;
	
	public async Task CreateRoleAsync(RoleRegistrationForm form)
	{
		var entity = RoleFactory.Create(form)!;
		await _roleRepository.AddAsync(entity);
	}

	public async Task<IEnumerable<Role>> GetRolesAsync()
	{
		var entity = await _roleRepository.GetAsync();
		return entity.Select(RoleFactory.Create)!;
	}

	public async Task UpdateRoleAsync(RoleUpdateForm form)
	{
		var entity = await _roleRepository.GetAsync(x => x.Id == form.Id) ?? throw new Exception("Role not found");

		RoleFactory.Update(entity, form);

		await _roleRepository.UpdateAsync(entity);
	}

	public async Task DeleteRoleAsync(int id)
	{
		var entity = await _roleRepository.GetAsync(x => x.Id == id);
		await _roleRepository.RemoveAsync(entity!);
	}
}