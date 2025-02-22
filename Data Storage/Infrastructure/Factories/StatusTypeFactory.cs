using Data.Entities;
using Infrastructure.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Factories;

public static class StatusTypeFactory
{
	public static StatusType? Create(StatusTypeEntity entity) => entity == null ? null : new()
	{
		Id = entity.Id,
		StatusName = entity.StatusName
	};
	
	public static StatusTypeEntity? Create(StatusTypeRegistrationForm form) => form == null ? null : new()
	{
		StatusName = form.StatusName
	};
	
	public static void Update(StatusTypeEntity entity, StatusTypeUpdateForm form)
	{
		entity.Id = form.Id;
		entity.StatusName = form.StatusName;
	}
}