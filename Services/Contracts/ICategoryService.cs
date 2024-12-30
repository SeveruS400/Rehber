using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICategoryService
    {
        IEnumerable<Categories> GetAllCategories(bool trackChanges);
        Task AddCategory(Categories category);
        int GetCategoryId(string categoryName);
    }
}
