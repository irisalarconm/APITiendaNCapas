using Microsoft.Extensions.Logging;
using ProjectCrud.DAL.Data.Repository;
using ProjectCrud.Models;
using Serilog;
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
        ILogger<ClientService> _logger;
        public ClientService(IGenericRepository<Client> clientRepository, ILogger<ClientService> logger)
        {
            _logger = logger;
            _clientRepository = clientRepository;   
        }
        public bool Create(Client client)
        {
            try
            {
                return _clientRepository.Create(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public string Delete(int id)
        {
            try
            {
                return _clientRepository.Delete(id); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ex.Message;
            }
            
        }

        public List<Client> GetAll()
        {
            try
            {
                return _clientRepository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
            
        }

        public Client GetById(int id)
        {
            try
            {
                return _clientRepository.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
            
        }

        public bool Update(Client client)
        {
            try
            {
                return _clientRepository.Update(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
            
        }
    }
}
