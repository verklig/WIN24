using Data.Repositories;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Dtos;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class StatusTypeService(StatusTypeRepository statusTypeRepository) : IStatusTypeService
{
	private readonly StatusTypeRepository _statusTypeRepository = statusTypeRepository;

	public async Task<bool> CreateStatusTypeAsync(StatusTypeRegistrationForm form)
	{
		var entity = StatusTypeFactory.Create(form);
		if (entity == null)
		{
			return false;
		}

		return await _statusTypeRepository.ExecuteInTransactionAsync(async () =>
		{
			await _statusTypeRepository.AddAsync(entity);
		});
	}

	public async Task<IEnumerable<StatusType?>> GetStatusTypesAsync()
	{
		var entities = await _statusTypeRepository.GetAsync();
		return entities?.Select(StatusTypeFactory.Create)!;
	}

	public async Task<StatusType?> GetStatusTypeByIdAsync(int id)
	{
		var entity = await _statusTypeRepository.GetAsync(x => x.Id == id);
		if (entity == null)
		{
			return null;
		}

		return StatusTypeFactory.Create(entity);
	}

	public async Task<bool> UpdateStatusTypeAsync(StatusTypeUpdateForm form)
	{
		var entity = await _statusTypeRepository.GetAsync(x => x.Id == form.Id);
		if (entity == null)
		{
			return false;
		}

		StatusTypeFactory.Update(entity, form);

		return await _statusTypeRepository.ExecuteInTransactionAsync(async () =>
		{
			await _statusTypeRepository.UpdateAsync(entity);
		});
	}

	public async Task<bool> DeleteStatusTypeAsync(int id)
	{
		var entity = await _statusTypeRepository.GetAsync(x => x.Id == id);
		if (entity == null)
		{
			return false;
		}

		return await _statusTypeRepository.ExecuteInTransactionAsync(async () =>
		{
			await _statusTypeRepository.RemoveAsync(entity);
		});
	}
}
