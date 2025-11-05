using MarketHup.Models.MVVM;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using System.Text;
using XSystem.Security.Cryptography;

namespace MarketHup.Models
{
    public class UserRepository : IUserRepository
    {
        Context con = new Context();


        public User? SelectMemberInfo(string Email)
        {


            return con.User.FirstOrDefault(u => u.Email == Email);

        }

        public bool AddUser(User user)
        {

            try
            {
                user.Active = true;
                user.IsAdmin = false;
                user.Password = MD5Şifrele(user.Password);
                con.User.Add(user);
                con.SaveChanges();


                return true;
            }
            catch (Exception)
            {
                return false;

            }


        }

        public static string MD5Şifrele(string value)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] btr = Encoding.UTF8.GetBytes(value);
            btr = md5.ComputeHash(btr);

            StringBuilder sb = new StringBuilder();
            foreach (byte item in btr)
            {
                sb.Append(item.ToString("x2").ToLower());
            }
            return sb.ToString();
        }

        public string LoginControl(User user)
        {
            user.Password = MD5Şifrele(user.Password);
            User? user1 = con.User.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);

            if (user1 != null)
            {
                if (user1.IsAdmin == true)
                {
                    return "admin";
                }
                else
                {
                    return user1.Email;
                }
            }
            else
            {
                return "error";
            }
        }

        
        public async Task<bool> LoginControl(string email)
        {
            User user = con.User.FirstOrDefault(u => u.Email == email);
            if (user == null) return false;
            return true;
        }
        public async Task<List<User>> Get() //Adminde Kullanıcıları listeleme
        {
            List<User> users = await con.User.ToListAsync();
           
            return users;
        }
        public async Task<User> Get(int id) //detay da düzenleme için
        {
            User users = await con.User.FirstOrDefaultAsync(u=>u.UserID==id);

            return users;
        }
        public bool Update(User user)
        {
            try
            {

                user.Password = MD5Şifrele(user.Password);
                con.Update(user);
                con.SaveChanges();

                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                User? user = con.User.FirstOrDefault(U => U.UserID == id);
                con.Remove(user);
                con.SaveChanges();
                return true;

                

               

            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}

