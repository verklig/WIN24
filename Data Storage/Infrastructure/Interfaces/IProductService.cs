using Infrastructure.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Interfaces;

public interface IProductService
{
	Task<bool> CreateProductAsync(ProductRegistrationForm form);
	Task<IEnumerable<Product?>> GetProductsAsync();
	Task<Product?> GetProductByIdAsync(int id);
	Task<bool> UpdateProductAsync(ProductUpdateForm form);
	Task<bool> DeleteProductAsync(int id);
}