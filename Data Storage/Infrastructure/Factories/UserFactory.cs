using Data.Entities;
using Infrastructure.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Factories;

public static class UserFactory
{
	public static User? Create(UserEntity entity) => entity == null ? null : new()
	{
		Id = entity.Id,
		FirstName = entity.FirstName,
		LastName = entity.LastName,
		Email = entity.Email,
		Role = entity.Role != null ? new Role { RoleName = entity.Role.RoleName } : null!,
	};
	
	public static UserEntity? Create(UserRegistrationForm form) => form == null ? null : new()
	{
		FirstName = form.FirstName,
		LastName = form.LastName,
		Email = form.Email,
		RoleId = form.RoleId
	};
	
	public static void Update(UserEntity entity, UserUpdateForm form)
	{
		entity.FirstName = form.FirstName;
		entity.LastName = form.LastName;
		entity.Email = form.Email;
		entity.RoleId = form.RoleId;
	}
}