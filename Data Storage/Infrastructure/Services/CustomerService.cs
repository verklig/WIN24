using Data.Repositories;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Dtos;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class CustomerService(CustomerRepository customerRepository) : ICustomerService
{
	private readonly CustomerRepository _customerRepository = customerRepository;

	public async Task<bool> CreateCustomerAsync(CustomerRegistrationForm form)
	{
		var entity = CustomerFactory.Create(form);
		if (entity == null)
		{
			return false;
		}
		
		return await _customerRepository.ExecuteInTransactionAsync(async () =>
		{
			await _customerRepository.AddAsync(entity);
		});
	}

	public async Task<IEnumerable<Customer?>> GetCustomersAsync()
	{
		var entities = await _customerRepository.GetAsync();
		return entities?.Select(CustomerFactory.Create)!;
	}

	public async Task<Customer?> GetCustomerByIdAsync(int id)
	{
		var entity = await _customerRepository.GetAsync(x => x.Id == id);
		if (entity == null)
		{
			return null;
		}

		return CustomerFactory.Create(entity);
	}

	public async Task<bool> UpdateCustomerAsync(CustomerUpdateForm form)
	{
		var entity = await _customerRepository.GetAsync(x => x.Id == form.Id);
		if (entity == null)
		{
			return false; 
		}

		CustomerFactory.Update(entity, form);

		return await _customerRepository.ExecuteInTransactionAsync(async () =>
		{
			await _customerRepository.UpdateAsync(entity);
		});
	}

	public async Task<bool> DeleteCustomerAsync(int id)
	{
		var entity = await _customerRepository.GetAsync(x => x.Id == id);
		if (entity == null)
		{
			return false;
		}

		return await _customerRepository.ExecuteInTransactionAsync(async () =>
		{
			await _customerRepository.RemoveAsync(entity);
		});
	}
}