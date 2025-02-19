using Data.Entities;
using Infrastructure.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Factories;

public static class ProjectFactory
{
	public static Project? Create(ProjectEntity entity) => entity == null ? null : new()
	{
		Id = entity.Id,
		Title = entity.Title,
		Description = entity.Description,
		StartDate = entity.StartDate,
		EndDate = entity.EndDate,
		StatusId = entity.StatusId,
		CustomerId = entity.CustomerId,
		ServiceId = entity.ServiceId,
		UserId = entity.UserId,
	};
	
	public static ProjectEntity? Create(ProjectRegistrationForm form) => form == null ? null : new()
	{
		Title = form.Title,
		Description = form.Description,
		StartDate = form.StartDate,
		EndDate = form.EndDate,
		StatusId = form.StatusId,
		CustomerId = form.CustomerId,
		ServiceId = form.ServiceId,
		UserId = form.UserId,
	};
	
	public static void Update(ProjectEntity entity, ProjectUpdateForm form)
	{
		entity.Title = form.Title;
		entity.Description = form.Description;
		entity.StartDate = form.StartDate;
		entity.EndDate = form.EndDate;
		entity.StatusId = form.StatusId;
		entity.CustomerId = form.CustomerId;
		entity.ServiceId = form.ServiceId;
		entity.UserId = form.UserId;
	}
}