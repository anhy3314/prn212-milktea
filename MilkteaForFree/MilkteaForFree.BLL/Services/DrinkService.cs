using MilkteaForFree.DAL.Entities;
using MilkteaForFree.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkteaForFree.BLL.Services
{
    public class DrinkService
    {
        private DrinkRepository _repo = new();

        public List<Drink> GetAllDrinks()
        {
            return _repo.GetAllDrinks();
        }

        public List<Drink> GetAllDrinksWithCategories()
        {
            return _repo.GetAllDrinksWithCategories();
        }

        public Drink GetById(int id) => _repo.GetById(id);

        public void AddDrink(Drink newDrink)
        {
            _repo.AddDrink(newDrink);
        }

        public void UpdateDrink(Drink drink) => _repo.UpdateDrink(drink);
        public void DeleteDrink(Drink drink) => _repo.DeleteDrink(drink);
        public List<Drink> SearchDrinkByName(string search) => _repo.SearchDrinkByName(search);
        public int CountDrink() => _repo.CountDrink();
    }

}
