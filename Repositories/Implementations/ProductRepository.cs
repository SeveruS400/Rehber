using Entities.Models;
using Entities.RequestParameters;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Extensions;
using System.Collections.Generic;

namespace Repositories.Implementations
{
    public class ProductRepository : RepositoryBase<Products>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context)
        {
        }

        public async Task CreateProduct(Products product) => await CreateAsync(product);

        public IEnumerable<Products> GetAllProducts(bool trackChanges)
        {
            // trackChanges true ise veriler izlenir, değilse AsNoTracking ile performans kazanılır
            var products = trackChanges
                ?  _context.Products.ToList()
                :  _context.Products.AsNoTracking().ToList();

            return products;
        }

        public IEnumerable<Products> GetAllProductsWithDetails(ProductRequestParameters p)
        {
            return _context
                .Products
                .Include(p=> p.Categories)
                .FilteredBySearchTerm(p.SearchTerm)
                .ToPaginate(p.PageNumber, p.PageSize);
        }

        public Products GetOneProduct(int id, bool trackChanges)
        {
            var product =  _context.Products.FirstOrDefault(u => u.Id == id);
            return product;
        }

        public IEnumerable<Products> GetShowCaseProducts(bool trackChanges)
        {
            return GetAll(trackChanges)
                .Where(p => p.ShowCase.Equals(true));
        }

        public async Task UpdateProduct(Products entity) => await UpdateAsync(entity.Id, entity);
        public void SaveProducts(List<Products> products)
        {
            _context.Products.AddRange(products);
            _context.SaveChanges();
        }
        public void UpdateProducts(List<Products> products)
        {
            _context.Products.UpdateRange(products);
            _context.SaveChanges();
        }

        public Products GetOneProductAsync(int id, bool trackChanges)
        {
            var product = _context.Products.FirstOrDefault(u => u.Id == id);
            return product;
        }

    }
}
