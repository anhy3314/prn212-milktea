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
        private readonly DrinkRepository _drinkRepository;

        public DrinkService()
        {
            _drinkRepository = new DrinkRepository();
        }

        public List<Drink> GetAllDrinks()
        {
            return _drinkRepository.GetAllDrinks();
        }

        public void AddDrink(Drink newDrink)
        {
            _drinkRepository.AddDrink(newDrink);
        }
    }

}
