using MarketHup.Models.MVVM;

namespace MarketHup.Models
{
    public interface IUserRepository
    {
        string LoginControl(User user);
        Task<bool> LoginControl(string email);
        User? SelectMemberInfo(string Email);
        bool AddUser(User user);
        Task<List<User>> Get();
        Task<User> Get(int id);
        bool Update(User user);
        bool Delete(int id);
    }
}
