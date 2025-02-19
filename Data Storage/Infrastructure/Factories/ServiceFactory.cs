using Data.Entities;
using Infrastructure.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Factories;

public static class ServiceFactory
{
	public static Service? Create(ServiceEntity entity) => entity == null ? null : new()
	{
		Id = entity.Id,
		ServiceName = entity.ServiceName,
		Price = entity.Price,
	};
	
	public static ServiceEntity? Create(ServiceRegistrationForm form) => form == null ? null : new()
	{
		ServiceName = form.ServiceName,
		Price = form.Price,
	};
	
	public static void Update(ServiceEntity entity, ServiceUpdateForm form)
	{
		entity.ServiceName = form.ServiceName;
		entity.Price = form.Price;
	}
}