using Data.Repositories;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Dtos;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class ProductService(ProductRepository productRepository) : IProductService
{
	private readonly ProductRepository _productRepository = productRepository;

	public async Task<bool> CreateProductAsync(ProductRegistrationForm form)
	{
		var entity = ProductFactory.Create(form);
		if (entity == null)
		{
			return false;
		}

		return await _productRepository.AddAsync(entity);
	}

	public async Task<IEnumerable<Product?>> GetProductsAsync()
	{
		var entities = await _productRepository.GetAsync();
		return entities?.Select(ProductFactory.Create)!;
	}

	public async Task<Product?> GetProductByIdAsync(int id)
	{
		var entity = await _productRepository.GetAsync(x => x.Id == id);
		if (entity == null)
		{
			return null;
		}

		return ProductFactory.Create(entity);
	}

	public async Task<bool> UpdateProductAsync(ProductUpdateForm form)
	{
		var entity = await _productRepository.GetAsync(x => x.Id == form.Id);
		if (entity == null)
		{
			return false;
		}

		ProductFactory.Update(entity, form);
		return await _productRepository.UpdateAsync(entity);
	}

	public async Task<bool> DeleteProductAsync(int id)
	{
		var entity = await _productRepository.GetAsync(x => x.Id == id);
		if (entity == null)
		{
			return false;
		}

		return await _productRepository.RemoveAsync(entity!);
	}
}
