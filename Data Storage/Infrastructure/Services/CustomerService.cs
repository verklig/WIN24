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
	
	public async Task UpdateCustomerAsync(CustomerUpdateForm form) 
	{ 
		var customerEntity = await _customerRepository.GetAsync(x => x.Id == form.Id) ?? throw new Exception("Customer not found");
		
		CustomerFactory.Update(customerEntity, form);

		await _customerRepository.UpdateAsync(customerEntity);
	}
	
	public async Task DeleteCustomerAsync(int id) 
	{
		var customerEntity = await _customerRepository.GetAsync(x => x.Id == id);
		await _customerRepository.RemoveAsync(customerEntity!);
	}
}