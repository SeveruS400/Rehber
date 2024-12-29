using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;

namespace Services.Contracts
{
    public interface IProductService
    {
        IEnumerable<Products> GetAllProducts(bool trackChanges);
        IEnumerable<Products> GetLastestProducts(int n, bool trackChanges);
        IEnumerable<Products> GetAllProductsWithDetails(ProductRequestParameters p);
        IEnumerable<Products> GetShowCaseProducts(bool trackChanges);
        Products? GetOneProduct(int id, bool trackChanges);
        void CreateProduct(ProductDtoForInsertion productDto);
        void UpdateProduct(ProductDtoForUpdate productDto);
        void DeleteProduct(int id);
        ProductDtoForUpdate GetOneProductForUpdate(int id, bool trackChanges);
    }
}
