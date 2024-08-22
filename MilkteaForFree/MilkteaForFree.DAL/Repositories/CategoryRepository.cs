using MilkteaForFree.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkteaForFree.DAL.Repositories
{
    public class CategoryRepository
    {
        private MilkTeaContext _context;
        public List<Category> GetAll()
        {
            _context = new MilkTeaContext();
            return _context.Categories.ToList();
        }
    }
}
