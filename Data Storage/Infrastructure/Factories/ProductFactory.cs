using Data.Entities;
using Infrastructure.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Factories;

public static class ProductFactory
{
	public static Product? Create(ProductEntity entity) => entity == null ? null : new()
	{
		Id = entity.Id,
		ProductName = entity.ProductName,
		Price = entity.Price,
	};
	
	public static ProductEntity? Create(ProductRegistrationForm form) => form == null ? null : new()
	{
		ProductName = form.ProductName,
		Price = form.Price,
	};
	
	public static void Update(ProductEntity entity, ProductUpdateForm form)
	{
		entity.ProductName = form.ProductName;
		entity.Price = form.Price;
	}
}