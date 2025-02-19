using Infrastructure.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Interfaces;

public interface ICustomerService
{
	Task CreateCustomerAsync(CustomerRegistrationForm form);
	Task DeleteCustomerAsync(int id);
	Task<Customer> GetCustomerByIdAsync(int id);
	Task<IEnumerable<Customer>> GetCustomersAsync();
	Task UpdateCustomerAsync(CustomerUpdateForm form);
}