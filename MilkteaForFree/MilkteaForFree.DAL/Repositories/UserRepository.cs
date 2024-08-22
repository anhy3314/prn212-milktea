using MilkteaForFree.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkteaForFree.DAL.Repositories
{
    public class UserRepository
    {
        //ko làm các hàm thêm, xóa, sửa user, ko làm hàm lấy hết các user
        //do đầu bài ko yêu cầu

        //ta chỉ làm hàm select * from StaffMember where = email-gõ và pass = pass-gõ
        //trả về dòng duy nhất nếu có - úng tính năng login

        MilkTeaContext _context; //ko new, chỉ new khi xài trực tiếp
        public User? GetOne(string name, string password)
        {
            _context = new();
            return _context.Users.FirstOrDefault(x => x.UserName.Equals(name) && x.Password.Equals(password));
            //trả về 1 dòng nếu tìm thấy, trả về null nếu ko thấy
        }

    }
}
