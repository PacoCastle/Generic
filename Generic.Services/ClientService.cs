
using System.Collections.Generic;
using System.Threading.Tasks;
using Generic.Core.Models;
using Generic.Core.Repositories;
using Generic.Core.Services;

namespace Generic.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClientService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Client> CreateClient(Client newClient)
        {
            await _unitOfWork.Clients.AddAsync(newClient);
            await _unitOfWork.CommitAsync();
            return newClient;
        }

        public async Task DeleteClient(Client Client)
        {
            _unitOfWork.Clients.Remove(Client);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            return await _unitOfWork.Clients
                .GetAllAsync();
        }

        public async Task<Client> GetClientById(int id)
        {
            return await _unitOfWork.Clients
                .GetByIdAsync(id);
        }

        public async Task UpdateClient(Client ClientToBeUpdated, Client Client)
        {
            ClientToBeUpdated.Name = Client.Name;
            ClientToBeUpdated.Address = Client.Address;

            await _unitOfWork.CommitAsync();
        }
    }
}