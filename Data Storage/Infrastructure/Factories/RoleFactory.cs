using Data.Entities;
using Infrastructure.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Factories;

public static class RoleFactory
{
	public static Role? Create(RoleEntity entity) => entity == null ? null : new()
	{
		Id = entity.Id,
		RoleName = entity.RoleName
	};
	
	public static RoleEntity? Create(RoleRegistrationForm form) => form == null ? null : new()
	{
		RoleName = form.RoleName
	};
	
	public static void Update(RoleEntity entity, RoleUpdateForm form)
	{
		entity.Id = entity.Id;
		entity.RoleName = entity.RoleName;
	}
}