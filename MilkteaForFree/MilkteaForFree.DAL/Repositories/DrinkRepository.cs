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
        public List<Drink> GetAllDrinks()
        {
            using (var context = new YourDbContext())
            {
                return context.Drinks.ToList();
            }
        }

        public void AddDrink(Drink newDrink)
        {
            using (var context = new YourDbContext())
            {
                context.Drinks.Add(newDrink);
                context.SaveChanges();
            }
        }
    }

}
