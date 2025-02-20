using Data.Repositories;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Dtos;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class StatusTypeService(StatusTypeRepository statusTypeRepository) : IStatusTypeService
{
	private readonly StatusTypeRepository _statusTypeRepository = statusTypeRepository;
	
	public async Task CreateStatusTypeAsync(StatusTypeRegistrationForm form)
	{
		var entity = StatusTypeFactory.Create(form)!;
		await _statusTypeRepository.AddAsync(entity);
	}

	public async Task<IEnumerable<StatusType>> GetStatusTypesAsync()
	{
		var entity = await _statusTypeRepository.GetAsync();
		return entity.Select(StatusTypeFactory.Create)!;
	}
	
	public async Task<StatusType> GetStatusTypeByIdAsync(int id)
	{
		var entity = await _statusTypeRepository.GetAsync(x => x.Id == id) ?? throw new Exception("Status Type not found");
		return StatusTypeFactory.Create(entity!)!;
	}

	public async Task UpdateStatusTypeAsync(StatusTypeUpdateForm form)
	{
		var entity = await _statusTypeRepository.GetAsync(x => x.Id == form.Id) ?? throw new Exception("Status Type not found");

		StatusTypeFactory.Update(entity, form);

		await _statusTypeRepository.UpdateAsync(entity);
	}

	public async Task DeleteStatusTypeAsync(int id)
	{
		var entity = await _statusTypeRepository.GetAsync(x => x.Id == id) ?? throw new Exception("Status Type not found");
		await _statusTypeRepository.RemoveAsync(entity!);
	}
}