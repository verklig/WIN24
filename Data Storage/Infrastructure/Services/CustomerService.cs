using Data.Repositories;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Dtos;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class CustomerService(CustomerRepository customerRepository) : ICustomerService
{
	private readonly CustomerRepository _customerRepository = customerRepository;

	public async Task CreateCustomerAsync(CustomerRegistrationForm form)
	{
		var entity = CustomerFactory.Create(form)!;
		await _customerRepository.AddAsync(entity);
	}

	public async Task<IEnumerable<Customer>> GetCustomersAsync()
	{
		var entity = await _customerRepository.GetAsync();
		return entity.Select(CustomerFactory.Create)!;
	}

	public async Task<Customer> GetCustomerByIdAsync(int id)
	{
		var entity = await _customerRepository.GetAsync(x => x.Id == id);
		return CustomerFactory.Create(entity!)!;
	}

	public async Task UpdateCustomerAsync(CustomerUpdateForm form)
	{
		var entity = await _customerRepository.GetAsync(x => x.Id == form.Id) ?? throw new Exception("Customer not found");

		CustomerFactory.Update(entity, form);

		await _customerRepository.UpdateAsync(entity);
	}

	public async Task DeleteCustomerAsync(int id)
	{
		var entity = await _customerRepository.GetAsync(x => x.Id == id);
		await _customerRepository.RemoveAsync(entity!);
	}
}