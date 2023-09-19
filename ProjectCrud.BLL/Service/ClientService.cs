using ProjectCrud.DAL.Data.Repository;
using ProjectCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCrud.BLL.Service
{
    public class ClientService : IClientService
    {
        private readonly IGenericRepository<Client> _clientRepository;
        public ClientService(IGenericRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;   
        }
        public bool Create(Client client)
        {
            return _clientRepository.Create(client);
        }

        public string Delete(int id)
        {
            return _clientRepository.Delete(id);
        }

        public List<Client> GetAll()
        {
            return _clientRepository.GetAll();
        }

        public Client GetById(int id)
        {
            return _clientRepository.GetById(id);
        }

        public bool Update(Client client)
        {
            return _clientRepository.Update(client);
        }
    }
}
