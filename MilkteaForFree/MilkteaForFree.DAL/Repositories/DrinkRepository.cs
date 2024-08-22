using Microsoft.EntityFrameworkCore;
using MilkteaForFree.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkteaForFree.DAL.Repositories
{
    public class DrinkRepository
    {
        private MilkTeaContext _context;
        public List<Drink> GetAllDrinks()
        {
            _context = new();
            return _context.Drinks.ToList();
        }

        public List<Drink> GetAllDrinksWithCategories()
        {
            _context = new();
            return _context.Drinks.Include("Category").ToList();
        }

        public Drink GetById(int id)
        {
            _context = new();
            return _context.Drinks.SingleOrDefault(x => x.DrinkId == id);
        }

        public void AddDrink(Drink newDrink)
        {
            _context = new();
            _context.Drinks.Add(newDrink);
            _context.SaveChanges();
        }

        public void UpdateDrink(Drink drink)
        {
            _context = new();
            _context.Update(drink);
            _context.SaveChanges();
        }

        public void DeleteDrink(Drink drink)
        {
            _context = new();
            _context.Remove(drink);
            _context.SaveChanges();
        }

        public List<Drink> SearchDrinkByName(string search)
        {
            _context = new();
            return _context.Drinks.Where(x => x.DrinkName.ToUpper().Contains(search.ToUpper())).ToList();
        }

        public int CountDrink()
        {
            _context = new();
            return _context.Drinks.Count();
        }
    }

}
