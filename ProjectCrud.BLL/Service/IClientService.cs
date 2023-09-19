using ProjectCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCrud.BLL.Service
{
    public interface IClientService
    {
        List<Client> GetAll();
        bool Create(Client model);
        Client GetById(int id);
        bool Update(Client model);
        string Delete(int id);
    }
}
