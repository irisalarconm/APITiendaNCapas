using ProjectCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCrud.BLL.Service
{
    public interface IProductService
    {
        List<Product> GetAll();
        bool Create(Product model);
        Product GetById(int id);
        bool Update(Product model);
        string Delete(int id);
    }
}
