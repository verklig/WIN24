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
		Status = entity.Status != null ? new StatusType { StatusName = entity.Status.StatusName } : null!,
		Customer = entity.Customer != null ? new Customer { CustomerName = entity.Customer.CustomerName } : null!,
		Product = entity.Product != null ? new Product { ProductName = entity.Product.ProductName, Price = entity.Product.Price } : null!,
		User = entity.User != null ? new User { FirstName = entity.User!.FirstName, LastName = entity.User.LastName } : null!,
	};
	
	public static ProjectEntity? Create(ProjectRegistrationForm form) => form == null ? null : new()
	{
		Title = form.Title,
		Description = form.Description,
		StartDate = form.StartDate,
		EndDate = form.EndDate,
		StatusId = form.StatusId,
		CustomerId = form.CustomerId,
		ProductId = form.ProductId,
		UserId = form.UserId
	};
	
	public static void Update(ProjectEntity entity, ProjectUpdateForm form)
	{
		entity.Title = form.Title;
		entity.Description = form.Description;
		entity.StartDate = form.StartDate;
		entity.EndDate = form.EndDate;
		entity.StatusId = form.StatusId;
		entity.CustomerId = form.CustomerId;
		entity.ProductId = form.ProductId;
		entity.UserId = form.UserId;
	}
}