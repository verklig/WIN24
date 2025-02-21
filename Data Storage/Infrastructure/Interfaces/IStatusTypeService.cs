using Infrastructure.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Interfaces;

public interface IStatusTypeService
{
	Task<bool> CreateStatusTypeAsync(StatusTypeRegistrationForm form);
	Task<IEnumerable<StatusType?>> GetStatusTypesAsync();
	Task<StatusType?> GetStatusTypeByIdAsync(int id);
	Task<bool> UpdateStatusTypeAsync(StatusTypeUpdateForm form);
	Task<bool> DeleteStatusTypeAsync(int id);
}