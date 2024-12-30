using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class CategoryManager : ICategoryService
    {
        private readonly IRepositoryManager _manager;

        public CategoryManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public IEnumerable<Categories> GetAllCategories(bool trackChanges)
        {
            return _manager.Category.GetAllCategories(trackChanges);
        }
        public async Task AddCategory(Categories category)
        {
            _manager.Category.AddCategory(category);
        }

        public int GetCategoryId(string categoryName)
        {
            return _manager.Category.GetCategoryId(categoryName);
        }
    }
}
