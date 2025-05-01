using Data.Entities;
using Data.Repositories;
using Domain.Extensions;
using Domain.Models;
using Domain.Dtos;
using Infrastructure.Models;

namespace Infrastructure.Services;

public interface IClientService
{
  Task<ClientResult> CreateClientAsync(AddClientFormData formData);
  Task<ClientResult<IEnumerable<Client>>> GetAllClientsAsync();
  Task<ClientResult<Client>> GetClientAsync(string clientId);
  Task<ClientResult> RemoveClientAsync(string clientId);
  Task<ClientResult> UpdateClientAsync(EditClientFormData formData);
}

public class ClientService(IClientRepository clientRepository) : IClientService
{
  private readonly IClientRepository _clientRepository = clientRepository;

  public async Task<ClientResult> CreateClientAsync(AddClientFormData formData)
  {
    if (formData == null)
    {
      return new ClientResult { Succeeded = false, StatusCode = 400, Error = "Not all required fields have a valid input." };
    }

    try
    {
      var entity = formData.MapTo<ClientEntity>();
      var result = await _clientRepository.AddAsync(entity);

      return result.Succeeded
        ? new ClientResult { Succeeded = true, StatusCode = 201 }
        : new ClientResult { Succeeded = false, StatusCode = 500, Error = "Unable to create client." };
    }
    catch (Exception ex)
    {
      return new ClientResult { Succeeded = false, StatusCode = 500, Error = ex.Message };
    }
  }

  public async Task<ClientResult> UpdateClientAsync(EditClientFormData formData)
  {
    if (formData == null)
    {
      return new ClientResult { Succeeded = false, StatusCode = 400, Error = "Not all required fields have a valid input." };
    }

    var result = await _clientRepository.GetEntityAsync(x => x.Id == formData.Id);
    if (!result.Succeeded || result.Result == null)
    {
      return new ClientResult { Succeeded = false, StatusCode = 404, Error = "Client not found." };
    }

    var clientEntity = result.Result;

    clientEntity.ClientName = formData.ClientName;
    clientEntity.Email = formData.Email;
    clientEntity.PhoneNumber = formData.PhoneNumber;
    clientEntity.Image = formData.Image;

    try
    {
      var updateResult = await _clientRepository.UpdateAsync(clientEntity);

      return updateResult.Succeeded
        ? new ClientResult { Succeeded = true, StatusCode = 200 }
        : new ClientResult { Succeeded = false, StatusCode = 500, Error = "Unable to update client." };
    }
    catch (Exception ex)
    {
      return new ClientResult { Succeeded = false, StatusCode = 500, Error = ex.Message };
    }
  }

  public async Task<ClientResult> RemoveClientAsync(string clientId)
  {
    if (string.IsNullOrEmpty(clientId))
    {
      return new ClientResult { Succeeded = false, StatusCode = 400, Error = "Client ID cannot be null or empty." };
    }

    var result = await _clientRepository.GetEntityAsync(x => x.Id == clientId);
    if (!result.Succeeded || result.Result == null)
    {
      return new ClientResult { Succeeded = false, StatusCode = 404, Error = "Client not found." };
    }

    var deleteResult = await _clientRepository.RemoveAsync(result.Result);
    return deleteResult.Succeeded
      ? new ClientResult { Succeeded = true, StatusCode = 200 }
      : new ClientResult { Succeeded = false, StatusCode = 500, Error = "Unable to delete client." };
  }

  public async Task<ClientResult<IEnumerable<Client>>> GetAllClientsAsync()
  {
    var result = await _clientRepository.GetAllAsync
    (
      orderByDecending: false,
      sortBy: s => s.Created
    );

    if (!result.Succeeded || result.Result == null)
    {
      return new ClientResult<IEnumerable<Client>> { Succeeded = false, StatusCode = 404, Error = "No clients found." };
    }

    return new ClientResult<IEnumerable<Client>> { Succeeded = true, StatusCode = 200, Result = result.Result };
  }

  public async Task<ClientResult<Client>> GetClientAsync(string clientId)
  {
    if (string.IsNullOrEmpty(clientId))
    {
      return new ClientResult<Client> { Succeeded = false, StatusCode = 400, Error = "Client ID cannot be null or empty." };
    }

    var result = await _clientRepository.GetAsync(x => x.Id == clientId);
    if (!result.Succeeded || result.Result == null)
    {
      return new ClientResult<Client> { Succeeded = false, StatusCode = 404, Error = "Client not found." };
    }

    return new ClientResult<Client> { Succeeded = true, StatusCode = 200, Result = result.Result };
  }
}
