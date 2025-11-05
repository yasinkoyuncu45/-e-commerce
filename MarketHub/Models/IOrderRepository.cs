using MarketHup.Models.MVVM;
using System.Drawing;

namespace MarketHup.Models
{
    public interface IOrderRepository
    {
        List<OrderRepository> SelectMyCart();
        List<Vw_MyOrder> SelectMyOrders(string Email);
        bool AddToMyCart(string id);
        void DeleteFromMyCart(string id);
        string OrderCreate(string Email);
        
       
    }
}
