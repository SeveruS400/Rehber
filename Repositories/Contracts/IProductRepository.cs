using Entities.Models;
using Entities.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IProductRepository : IRepositoryBase<Products>
    {
        IEnumerable<Products> GetAllProducts(bool trackChanges);
        IEnumerable<Products> GetAllProductsWithDetails(ProductRequestParameters p);
        IEnumerable<Products> GetShowCaseProducts(bool trackChanges);
        Products GetOneProduct(int id, bool trackChanges);
        Task CreateProduct(Products product);
        Task UpdateProduct(Products entity);
    }
}
