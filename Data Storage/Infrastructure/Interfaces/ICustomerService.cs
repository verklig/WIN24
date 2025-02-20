using Infrastructure.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Interfaces;

public interface ICustomerService
{
	Task CreateCustomerAsync(CustomerRegistrationForm form);
	Task<IEnumerable<Customer>> GetCustomersAsync();
	Task<Customer> GetCustomerByIdAsync(int id);
	Task UpdateCustomerAsync(CustomerUpdateForm form);
	Task DeleteCustomerAsync(int id);
}