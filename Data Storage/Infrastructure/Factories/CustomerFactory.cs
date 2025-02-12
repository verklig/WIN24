using Data.Entities;
using Infrastructure.Models;

namespace Infrastructure.Factories;

public static class CustomerFactory
{
	public static Customer? Create(CustomerEntity entity) => entity == null ? null : new()
	{
		Id = entity.Id,
		CustomerName = entity.CustomerName,
	};
	
	public static CustomerEntity? Create(CustomerRegistrationForm form) => form == null ? null : new()
	{
		CustomerName = form.CustomerName,
	};
}