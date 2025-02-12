using Data.Repositories;
using Infrastructure.Factories;
using Infrastructure.Models;

namespace Infrastructure.Services;

public class CustomerService(CustomerRepository customerRepository)
{
	private readonly CustomerRepository _customerRepository = customerRepository;
	
	public async Task CreateCustomerAsync(CustomerRegistrationForm form) 
	{
		var customerEntity = CustomerFactory.Create(form)!;
		await _customerRepository.AddAsync(customerEntity);
	}
	
	public async Task<IEnumerable<Customer>> GetCustomersAsync() 
	{ 
		var customerEntities = await _customerRepository.GetAsync();
		return customerEntities.Select(CustomerFactory.Create)!;
	}

	public async Task<Customer> GetCustomerByIdAsync(int id) 
	{ 
		var customerEntity = await _customerRepository.GetAsync(x => x.Id == id);
		return CustomerFactory.Create(customerEntity!)!;
	}
	
	public async Task UpdateCustomerAsync(Customer customer) 
	{ 
		
	}
	
	public async Task DeleteCustomerAsync(int id) 
	{
		
	}
}