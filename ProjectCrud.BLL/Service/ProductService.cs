using ProjectCrud.DAL.Data.Repository;
using ProjectCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCrud.BLL.Service
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        public ProductService(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public bool Create(Product product)
        {
            return _productRepository.Create(product);
        }

        public string Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public bool Update(Product product)
        {
            return _productRepository.Update(product); 
        }
    }
}
