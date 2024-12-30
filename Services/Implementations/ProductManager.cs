using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using Repositories.Contracts;
using Services.Contracts;

namespace Services.Implementations
{
    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ProductManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public void CreateProduct(ProductDtoForInsertion productDto)
        {
            Products product = _mapper.Map<Products>(productDto);
            _manager.Product.CreateAsync(product);
            _manager.Save();
        }

        public void DeleteProduct(int id)
        {
             _manager
                .Product
                .DeleteAsync(id);
            _manager.Save();
        }

        public IEnumerable<Products> GetAllProducts(bool trackChanges)
        {
            return _manager
                .Product
                .GetAllProducts(trackChanges);
        }

        public IEnumerable<Products> GetAllProductsWithDetails(ProductRequestParameters p)
        {
            return _manager
                .Product
                .GetAllProductsWithDetails(p);
        }

        public IEnumerable<Products> GetLastestProducts(int n, bool trackChanges)
        {
            return _manager
                .Product
                .GetAll(trackChanges)
                .OrderByDescending(prd => prd.Id)
                .Take(n);
        }

        public Products? GetOneProduct(int id, bool trackChanges)
        {
            var product = _manager.Product.GetOneProduct(id, trackChanges);
            if (product is null)
                throw new Exception("Product not found!");
            return product;
        }

        public ProductDtoForUpdate GetOneProductForUpdate(int id, bool trackChanges)
        {
            var product =  _manager.Product.GetOneProduct(id, trackChanges);
            if (product == null)
            {
                // Eğer ürün bulunamazsa null kontrolü yapıyoruz
                throw new Exception("Product not found");
            }

            var productDto = _mapper.Map<ProductDtoForUpdate>(product);
            return productDto;
        }

        public IEnumerable<Products> GetShowCaseProducts(bool trackChanges)
        {
            var products = _manager.Product.GetShowCaseProducts(trackChanges);
            return products;
        }

        public void SaveProducts(List<Products> products)
        {
            _manager.Product.SaveProducts(products);
        }

        public void UpdateProduct(ProductDtoForUpdate productDto)
        {
            var entity =  _manager.Product.GetOneProduct(productDto.Id, true);
            entity.Id = productDto.Id;
            entity.Name = productDto.Name;
            entity.SurName = productDto.SurName;
            entity.Address = productDto.Address;
            entity.PhoneNumber = productDto.PhoneNumber;
            entity.CategoryId = productDto.CategoryId;
			entity.EducationStatusId = productDto.EducationStatusId;
            entity.ReferanceId = productDto.ReferanceId;


			_manager.Save();
        }
    }
}
