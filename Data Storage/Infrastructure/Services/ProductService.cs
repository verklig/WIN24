using Data.Repositories;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Services;

public class ProductService(ProductRepository productRepository)
{
	private readonly ProductRepository _productRepository = productRepository;
	
	public async Task CreateProductAsync(ProductRegistrationForm form)
	{
		var entity = ProductFactory.Create(form)!;
		await _productRepository.AddAsync(entity);
	}

	public async Task<IEnumerable<Product>> GetRolesAsync()
	{
		var entity = await _productRepository.GetAsync();
		return entity.Select(ProductFactory.Create)!;
	}
	
	public async Task<Product> GetProductsByIdAsync(int id)
	{
		var entity = await _productRepository.GetAsync(x => x.Id == id) ?? throw new Exception("Product not found");
		return ProductFactory.Create(entity!)!;
	}

	public async Task UpdateProductAsync(ProductUpdateForm form)
	{
		var entity = await _productRepository.GetAsync(x => x.Id == form.Id) ?? throw new Exception("Product not found");

		ProductFactory.Update(entity, form);

		await _productRepository.UpdateAsync(entity);
	}

	public async Task DeleteProductAsync(int id)
	{
		var entity = await _productRepository.GetAsync(x => x.Id == id) ?? throw new Exception("Product not found");
		await _productRepository.RemoveAsync(entity!);
	}
}