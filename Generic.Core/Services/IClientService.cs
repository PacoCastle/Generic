using Generic.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Core.Services
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetAllClients();
        Task<Client> GetClientById(int id);
        Task<Client> CreateClient(Client newClient);
        Task UpdateClient(Client ClientToBeUpdated, Client Client);
        Task DeleteClient(Client Client);
    }
}
