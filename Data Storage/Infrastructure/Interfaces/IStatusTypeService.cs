using Infrastructure.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Interfaces;

public interface IStatusTypeService
{
	Task CreateStatusTypeAsync(StatusTypeRegistrationForm form);
	Task<IEnumerable<StatusType>> GetStatusTypesAsync();
	Task<StatusType> GetStatusTypeByIdAsync(int id);
	Task UpdateStatusTypeAsync(StatusTypeUpdateForm form);
	Task DeleteStatusTypeAsync(int id);
}