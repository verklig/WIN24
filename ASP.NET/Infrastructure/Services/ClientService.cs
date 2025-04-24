using Infrastructure.Models;
using Data.Repositories;
using Domain.Extensions;

namespace Infrastructure.Services;

public interface IClientService
{
  Task<ClientResult> GetClientResultAsync();
}

public class ClientService(IClientRepository clientRepository) : IClientService
{
  private readonly IClientRepository _clientRepository = clientRepository;
  
  public async Task<ClientResult> GetClientResultAsync()
  {
    var result = await _clientRepository.GetAllAsync();
    return result.MapTo<ClientResult>();
  }
}