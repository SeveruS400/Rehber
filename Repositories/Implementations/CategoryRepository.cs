using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class CategoryRepository : RepositoryBase<Categories>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context)
        {
        }

        public async Task AddCategory(Categories category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public int GetCategoryId(string categoryName)
        {
            var category = _context.Categories.FirstOrDefault(r => r.CategoryName == categoryName);

            // Kategori bulunduysa, Id'yi döndür
            if (category != null)
            {
                return category.Id;
            }

            // Kategori bulunamadıysa, uygun bir değer döndür
            // Örneğin -1 ya da null döndürebilirsiniz, ihtiyaçlarınıza göre
            return -1;
        }
        public IEnumerable<Categories> GetAllCategories(bool trackChanges)
        {
            // trackChanges true ise veriler izlenir, değilse AsNoTracking ile performans kazanılır
            var Categories = trackChanges
                ?  _context.Categories.ToList()
                :  _context.Categories.AsNoTracking().ToList();

            return Categories;
        }
    
    }
}
