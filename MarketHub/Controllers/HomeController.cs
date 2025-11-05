using Microsoft.AspNetCore.Mvc;
using MarketHup.Models.MVVM;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MarketHup.Models;
using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;


using X.PagedList;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace MarketHup.Controllers
{
    public class HomeController : Controller
    {
        public static string OrderGroupGUID = "";
        ProductRepository productRepository = new ProductRepository();
        OrderRepository orderRepository = new OrderRepository();
        UserRepository userRepository = new UserRepository();
        CommunicationRepository communicationRepository = new CommunicationRepository();
        MainPageModel mpm = new MainPageModel();
        Context con = new Context();
        public IActionResult Index()
        {

            mpm.SliderProducts = productRepository.Get("Slider", -1);
            mpm.NewProducts = productRepository.Get("New", -1);
            mpm.Productofday = productRepository.Get("Day");
            mpm.SpecialProducts = productRepository.Get("Special", -1);
            mpm.DiscountedProducts = productRepository.Get("Discounted", -1);
            mpm.HighlightedProducts = productRepository.Get("Highlighted", -1);
            mpm.TopsellerProducts = productRepository.Get("Topseller", -1);
            mpm.StarProducts = productRepository.Get("Star", -1);
            mpm.OpportunityProducts = productRepository.Get("Opportunity", -1);
            mpm.NoTableProducts = productRepository.Get("NoTable", -1);
            mpm.PopulerProducts = productRepository.Get("Populer", -1);


            return View(mpm);
        }

        public IActionResult NewProducts()
        {

            mpm.NewProducts = productRepository.Get("New", 0);
            mpm.TotalProductCount = productRepository.TotalProduct("New");

            return View(mpm);
        }
        public PartialViewResult _PartialNewProducts(int pageno)
        {
            mpm.NewProducts = productRepository.Get("New", pageno);

            return PartialView(mpm);
        }

        public IActionResult SpecialProducts()
        {

            mpm.SpecialProducts = productRepository.Get("Special", 0);
            mpm.TotalProductCount = productRepository.TotalProduct("Special");

            return View(mpm);
        }
        public PartialViewResult _PartialSpecialProducts(int pageno)
        {
            mpm.SpecialProducts = productRepository.Get("Special", pageno);

            return PartialView(mpm);
        }

        public IActionResult DiscountedProducts()
        {

            mpm.DiscountedProducts = productRepository.Get("Discounted", 0);

            return View(mpm);
        }
        public PartialViewResult _PartialDiscountedProducts(int pageno)
        {
            mpm.DiscountedProducts = productRepository.Get("Discounted", pageno);

            return PartialView(mpm);
        }

        public IActionResult HighLightedProducts()
        {

            mpm.HighlightedProducts = productRepository.Get("Highlighted", 0);

            return View(mpm);
        }
        public PartialViewResult _PartialHighLightedProducts(int pageno)
        {
            mpm.HighlightedProducts = productRepository.Get("Highlighted", pageno);

            return PartialView(mpm);
        }
        public IActionResult TopsellerProducts(int page = 1)
        {

            productRepository.page = page;
            productRepository.subpageCount = 4;
            PagedList<Product> model = productRepository.TopsellerProductList();

            return View("TopsellerProducts", model);
        }

        public IActionResult CategoryPage(int id, int pagenumber)
        {

            mpm.CategoryPage = productRepository.Get("Category", id, pagenumber);

            return View(mpm);

        }
        public PartialViewResult _PartialCategoryPage(int id, int pagenumber)
        {
            mpm.CategoryPage = productRepository.Get("Category", id, pagenumber);

            return PartialView(mpm);
        }
        public IActionResult SupplierPage(int id, int pagenumber)
        {
            mpm.SupplierPage = productRepository.Get("Supplier", id, pagenumber);

            return View(mpm);

        }

        public PartialViewResult _PartialSupplierPage(int id, int pagenumber)
        {
            mpm.SupplierPage = productRepository.Get("Supplier", id, pagenumber);
            return PartialView(mpm);
        }

        public IActionResult CartProcess(int id)
        {
            string regererUrl = Request.Headers["Referer"].ToString();
            string url = "";

            if (id > 0)
            {
                productRepository.Highlighted_Increase(id);
                orderRepository.ProductID = id;
                orderRepository.Quantity = 1;
                var cookieOptions = new CookieOptions();
                var cookie = Request.Cookies["Sepetim"];
                if (cookie == null)
                {
                    cookieOptions.Expires = DateTime.Now.AddDays(1);
                    cookieOptions.Path = "/";
                    orderRepository.MyCart = "";
                    orderRepository.AddToMyCart(id.ToString());
                    Response.Cookies.Append("Sepetim", orderRepository.MyCart, cookieOptions);
                    TempData["Message"] = "Ürün Sepetinize Eklendi";
                }
                else
                {
                    orderRepository.MyCart = cookie;
                    if (orderRepository.AddToMyCart(id.ToString()) == false)
                    {
                        HttpContext.Response.Cookies.Append("Sepetim", orderRepository.MyCart, cookieOptions);
                        cookieOptions.Expires = DateTime.Now.AddDays(1);
                        TempData["Message"] = "Ürün Sepetinize Eklendi";
                    }
                    else
                    {
                        TempData["Message"] = "Ürün Sepetinize Var";
                    }
                }
                Uri refererUri = new Uri(regererUrl, UriKind.Absolute);
                url = refererUri.AbsolutePath;
                if (url.Contains("DpProducts") || regererUrl.Contains("http://localhost:7014"))
                {
                    return RedirectToAction("Index");

                }
                return Redirect(url);
            }
            else
            {
                Uri refererUri = new Uri(regererUrl, UriKind.Absolute);
                url = refererUri.AbsolutePath;
                if (url.Contains("DpProducts"))
                {
                    return RedirectToAction("Index");

                }
                return Redirect(url);
            }
        }

        public IActionResult Cart()
        {


            if (HttpContext.Request.Query["ProductID"].ToString() != "")
            {
                string ProductID = HttpContext.Request.Query["ProductID"].ToString();
                orderRepository.MyCart = Request.Cookies["Sepetim"];
                orderRepository.DeleteFromMyCart(ProductID.ToString());
                var cookieOptions = new CookieOptions();
                Response.Cookies.Append("Sepetim", orderRepository.MyCart, cookieOptions);
                cookieOptions.Expires = DateTime.Now.AddDays(1);
                TempData["Message"] = "Ürün Sepetten Silindi";
                ViewBag.Sepetim = orderRepository.SelectMyCart();
            }
            else
            {
                var cookie = Request.Cookies["Sepetim"];
                if (cookie == null)
                {
                    orderRepository.MyCart = "";
                    ViewBag.Sepetim = orderRepository.SelectMyCart();
                }
                else
                {
                    orderRepository.MyCart = Request.Cookies["Sepetim"];
                    ViewBag.Sepetim = orderRepository.SelectMyCart();
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Order()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login");

            }
            else
            {
                User? usr = userRepository.SelectMemberInfo(HttpContext.Session.GetString("Email"));
                if (usr != null)
                {
                    return View(usr);
                }
            }
            return View();

        }
        [HttpPost]
        public IActionResult Order(IFormCollection frm)
        {
            string kredikartno = Request.Form["kredikartno"];
            string kredikartay = Request.Form["kredikartay"];
            string kredikartyıl = Request.Form["kredikartyıl"];
            string kredikartcvs = Request.Form["kredikartcvs"];

            string txt_individual = frm["txt_individual"];
            string txt_corporate = frm["txt_corporate"];

            if (txt_individual != null)
            {
                //bireysel fatura 
                //orderRepository.tckimlik_vergi_no=txt_individual;
                //orderRepository.EfaturaCreate();
            }
            else
            {
                // kurumsal fatura
            }
            return RedirectToAction("backref");

        }
        public IActionResult backref()
        {
            var cookieOptions = new CookieOptions();
            var cookie = Request.Cookies["Sepetim"];
            if (cookie != null)
            {
                orderRepository.MyCart = cookie;
                OrderGroupGUID = orderRepository.OrderCreate(HttpContext.Session.GetString("Email").ToString());
                Response.Cookies.Delete("Sepetim");


            }

            return RedirectToAction("ConfirmPage");
        }

        public IActionResult ConfirmPage()
        {
            ViewBag.OrderGroupGUID = OrderGroupGUID;
            return View();

        }


        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            string answer = userRepository.LoginControl(user);

            if (answer == "error")
            {
                TempData["Message"] = "Email/Şifre yanlış girildi";
                return View();
            }
            else if (answer.Contains("@"))
            {
                HttpContext.Session.SetString("Email", answer);
                return RedirectToAction("Index");
            }
            else
            {
                HttpContext.Session.SetString("Email", answer);
                HttpContext.Session.SetString("Admin", answer);
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                bool usr =await userRepository.LoginControl(user.Email);
                if (usr == false)
                {
                    bool answer = userRepository.AddUser(user);
                    if (answer=true)
                    {
                        TempData["Message"] = "Kaydedildi.";
                        return RedirectToAction("Login");

                    }
                    TempData["Message"] = "Hata Tekrar Deneyiniz.";


                }
                else
                {
                    TempData["Message"] = "Bu Email Zaten Kayıtlı";


                }
            }
            return View();
        }

        public IActionResult AboutUs()
        {
            var aboutUs = con.AboutUs.FirstOrDefault(); // İlk kaydı al
            return View(aboutUs);
        }

        public IActionResult MyOrders()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                List<Vw_MyOrder> orders = orderRepository.SelectMyOrders(HttpContext.Session.GetString("Email").ToString());
                return View(orders);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public static int staticid = 0;
        public IActionResult Details(int id)
        {

            if (id > 0)
            {
                staticid = id;
            }
            else
            {
                id = staticid;
            }
            productRepository.Highlighted_Increase(id); // highlighted kolonunu arttır metodunu cağırdım
            //entity f.core
            //mpm.ProductDetails=contex.Product.FirsOrDefault(p=>p.ProductID==id);

            //ado.net ,dapper
            //Selet * from Prdocuts where ProductID=id


            //linq
            mpm.Productdetails = (from p in con.Product where p.ProductID == id select p).FirstOrDefault();

            mpm.CategoryName = (from p in con.Product
                                join c in con.Category
                                on p.CategoryID equals c.CategoryID
                                where p.ProductID == id
                                select c.CategoryName).FirstOrDefault();


            mpm.BrandName = (from p in con.Product
                             join s in con.Supplier
                             on p.SupplierID equals s.SupplierID
                             where p.ProductID == id
                             select s.BrandName).FirstOrDefault();

            mpm.RelatedProducts = con.Product.Where(p => p.Related == mpm.Productdetails!.Related && p.ProductID != id).ToList();
            return View(mpm);
        }
        public IActionResult ContactUs()
        {
            Communication communication = communicationRepository.Get();

            if (communication == null)
            {
                communication = new Communication(); // boş nesne gönder ki View patlamasın
            }

            return View(communication);
        }

        public IActionResult DetailedSearch()
        {
            ViewBag.Categories = con.Category.ToList();
            ViewBag.Suppliers = con.Supplier.ToList();
            return View();

        }

        [HttpPost]
        public IActionResult DProducts(int CategoryID, int[] SupplierID, string price, string IsInStock)
        {
            price = price.Replace(" ", "").Replace("TL", "");
            string[] PriceArry = price.Split('-');
            string startprice = PriceArry[0];
            string endprice = PriceArry[1];

            string sign = ">";
            if (IsInStock == "0")
            {
                sign = ">";
            }

            string suppliervalue = "";
            for (int i = 0; i < SupplierID.Length; i++)
            {
                if (i == 0)
                {
                    suppliervalue = "SupplierID =" + SupplierID[i];
                }
                else
                {
                    suppliervalue += " or SupplierID =" + SupplierID[i];
                }
            }
            string query = "select * from Product where CategoryID=" + CategoryID + " and (" + suppliervalue + ") and (UnitPrice>= " +
                startprice + " and UnitPrice<= " + endprice + ") and [stock] " + sign + " 0 order by UnitPrice";

            ViewBag.Products = productRepository.SelectProductByDetails(query);

            return View();
        }

        public PartialViewResult gettingSearch(string id)
        {
            id = id.ToUpper(new System.Globalization.CultureInfo("tr-TR"));
            List<Sp_Search> ulist = productRepository.gettingSearchProduct(id);
            string json = JsonConvert.SerializeObject(ulist);
            var response = JsonConvert.DeserializeObject<List<Search>>(json);
            return PartialView(response);
        }
    }
}
