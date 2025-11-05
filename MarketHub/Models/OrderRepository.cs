using MarketHup.Models.MVVM;

namespace MarketHup.Models
{
    public class OrderRepository:IOrderRepository
    {
        Context con = new Context();
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string MyCart { get; set; } // sepetteki ürünleri tutacağız
        public decimal UnitPrice { get; set; }
        public string ProductName { get; set; }
        public string PhotoPath { get; set; }
        public int Kdv { get; set; }
        public bool AddToMyCart(string id)
        {
            bool exists = false;

            if (MyCart == "") // sepete ilk defa ürün ekleneceğinde buraya girecek
            {
                MyCart = id + "=" + Quantity; //10=1
            }
            else
            {
                string[] MycartArray = MyCart.Split('£');
                for (int i = 0; i < MycartArray.Length; i++)
                {
                    string[] MycartArrayLoop = MycartArray[i].Split('='); // mycartarray i bidaha ayırdık MycartArrayLoop un 1. alanında 10 var 2. alanında 1

                    if (MycartArrayLoop[0] == id)
                    {
                        exists = true; // true da sepette var mesajı verecek
                        // MycartArrayLoop[1] += 1;    arttırmak istiyorsak bunu aktif et mesajı değiş
                    }
                }
                if (exists == false)
                {
                    MyCart = MyCart + "£" + id.ToString() + "=1";
                }

            }
            return exists;
        }
        public List<OrderRepository> SelectMyCart()
        {
            List<OrderRepository> List = new List<OrderRepository>();
            string[] MycartArray = MyCart.Split('£');
            if (MyCart != "")
            {
                for (int i = 0; i < MycartArray.Length; i++)
                {
                    string[] MycartArrayLoop = MycartArray[i].Split('=');
                    int ProductID = Convert.ToInt32(MycartArrayLoop[0]);
                    Product? product = con.Product.FirstOrDefault(p => p.ProductID == ProductID);
                    OrderRepository order = new OrderRepository();
                    order.ProductID = ProductID;
                    order.Quantity = Convert.ToInt32(MycartArrayLoop[1]);
                    order.UnitPrice = product.UnitPrice;
                    order.ProductName = product.ProductName;
                    order.PhotoPath = product.PhotoPath;
                    order.Kdv = product.Kdv;
                    List.Add(order);
                }
            }
            return List;
        }

        public void DeleteFromMyCart(string id)
        {
            string[] MycartArray = MyCart.Split('£');
            string NewMyCart = "";
            int count = 1;

            for (int i = 0; i < MycartArray.Length; i++)
            {
                string[] MycartArrayLoop = MycartArray[i].Split('=');
                string ProductID = MycartArrayLoop[0];

                if (ProductID != id)
                {
                    //silinmeyecekler
                    if (count == 1)
                    {
                        NewMyCart = MycartArrayLoop[0] + "=" + MycartArrayLoop[1];
                        count++;
                    }
                    else
                    {
                        NewMyCart += "£" + MycartArrayLoop[0] + "=" + MycartArrayLoop[1];
                    }
                }
            }
            MyCart = NewMyCart;

        }


        public string OrderCreate(string Email)
        {
            List<OrderRepository> siplist = SelectMyCart();
            string OrderGroupGUID = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(".", "");
            DateTime OrderDate = DateTime.Now;
            foreach (var item in siplist)
            {
                Order order = new Order();
                order.OrderDate = OrderDate;
                order.OrderGroupGUID = OrderGroupGUID;
                order.UserID = con.User.FirstOrDefault(u => u.Email == Email).UserID;
                order.ProductID = item.ProductID;
                order.Quantity = item.Quantity;
                con.Order.Add(order);
                con.SaveChanges();
            }
            return OrderGroupGUID;

        }

        public List<Vw_MyOrder> SelectMyOrders(string Email)
        {
            int UserID = con.User.FirstOrDefault(U => U.Email == Email).UserID;
            List<Vw_MyOrder> myOrders = con.Vw_MyOrders.Where(o => o.UserID == UserID).ToList();
            return myOrders;


        }
    }
}
