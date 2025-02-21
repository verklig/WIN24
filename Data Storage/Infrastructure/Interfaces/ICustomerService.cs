using Infrastructure.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Interfaces;

public interface ICustomerService
{
	Task<bool> CreateCustomerAsync(CustomerRegistrationForm form);
	Task<IEnumerable<Customer?>> GetCustomersAsync();
	Task<Customer?> GetCustomerByIdAsync(int id);
	Task<bool> UpdateCustomerAsync(CustomerUpdateForm form);
	Task<bool> DeleteCustomerAsync(int id);
}