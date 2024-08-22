using MilkteaForFree.DAL.Entities;
using MilkteaForFree.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkteaForFree.BLL.Services
{
    public class CategoryService
    {
        private CategoryRepository _repo = new();
        public List<Category> GetAllCategories() => _repo.GetAll();
    }
}
