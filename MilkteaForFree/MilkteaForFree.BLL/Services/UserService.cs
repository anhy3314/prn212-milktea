using MilkteaForFree.DAL.Entities;
using MilkteaForFree.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkteaForFree.BLL.Services
{
    public class UserService
    {
        private UserRepository _repo = new();

        public User? Authenticate(string name, string password) => _repo.GetOne(name, password);
    }
}
