using Infrastructure.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Interfaces;

public interface IRoleService
{
	Task CreateRoleAsync(RoleRegistrationForm form);
	Task<IEnumerable<Role>> GetRolesAsync();
	Task<Role> GetRoleByIdAsync(int id);
	Task UpdateRoleAsync(RoleUpdateForm form);
	Task DeleteRoleAsync(int id);
}