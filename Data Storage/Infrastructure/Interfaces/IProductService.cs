using Infrastructure.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Interfaces;

public interface IProductService
{
	Task CreateProductAsync(ProductRegistrationForm form);
	Task<IEnumerable<Product>> GetProductsAsync();
	Task<Product> GetProductByIdAsync(int id);
	Task UpdateProductAsync(ProductUpdateForm form);
	Task DeleteProductAsync(int id);
}