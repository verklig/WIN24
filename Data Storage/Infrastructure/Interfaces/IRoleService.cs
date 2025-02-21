using Infrastructure.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Interfaces;

public interface IRoleService
{
	Task<bool> CreateRoleAsync(RoleRegistrationForm form);
	Task<IEnumerable<Role?>> GetRolesAsync();
	Task<Role?> GetRoleByIdAsync(int id);
	Task<bool> UpdateRoleAsync(RoleUpdateForm form);
	Task<bool> DeleteRoleAsync(int id);
}