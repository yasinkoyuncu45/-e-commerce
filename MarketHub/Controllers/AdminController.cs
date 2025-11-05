using MarketHup.Models;
using MarketHup.Models.MVVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MarketHup.Controllers
{
    public class AdminController : Controller
    {
        UserRepository userRepository = new UserRepository();
        CategoryRepository categoryRepository = new CategoryRepository();
        Context con = new Context();
        SupplierRepository supplierRepository = new SupplierRepository();
        StatusRepository statusRepository = new StatusRepository();
        ProductRepository productRepository = new ProductRepository();
        AboutUsRepository aboutUsRepository = new AboutUsRepository();
        CommunicationRepository communicationRepository = new CommunicationRepository();
        void CategoryFill(string main_or_all)
        {
            List<Category> categories = categoryRepository.Get(main_or_all);
            ViewData["categoryList"] = categories.Select(c => new SelectListItem { Text = c.CategoryName, Value = c.CategoryID.ToString() });
        }
        void SupplierFill()
        {
            List<Supplier> suppliers = supplierRepository.Get();
            ViewData["supplierList"] = suppliers.Select(c => new SelectListItem
            {
                Text = c.BrandName,
                Value = c.SupplierID.ToString()
            });
        }
        void StatusFill()
        {
            List<Status> statuses = statusRepository.Get();
            ViewData["statusList"] = statuses.Select(c => new SelectListItem { Text = c.StatusName, Value = c.StatusID.ToString() });
        }


        [HttpGet]
        public IActionResult Login   ()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("Email,Password,NameSurname")] User user)
        {
            if (ModelState.IsValid)
            {
                //zorunlu alan ok ise buraya girer
                string usr = userRepository.LoginControl(user);
                if (usr == "admin")
                {
                    HttpContext.Session.SetString("Admin", "AdminValue");
                    return RedirectToAction("Index"); //Admin/Index sayfasına gider
                }
                else
                {
                    TempData["Message"] = "Login ve/veya şifre yanlış";
                }
            }
            return View(); //aynı sayfada kalır ,tekrar giriş yapar.    
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CategoryIndex()
        {
            List<Category> categories = categoryRepository.Get("all");
            return View(categories);
        }

        [HttpGet]
        public IActionResult CategoryCreate()
        {
            CategoryFill("main");
            return View();
        }

        [HttpPost]
        public IActionResult CategoryCreate(Category category)
        {
            if (ModelState.IsValid)//bütün zorunlu alanların kontrolü
            {
                bool answer = categoryRepository.Create(category); 
                if (answer == true)
                {
                    TempData["Message"] = category.CategoryName + " Kategorisi Eklendi";
                }
                else
                {
                    TempData["Message"] = "HATA";
                }
            }
            return RedirectToAction("CategoryCreate"); //[HttpGet] metoduna gider.
        }

        public static int? staticid = 0;
        [HttpGet]
        public async Task<IActionResult> CategoryEdit(int? id)
        {
            if (id > 0)
            {
                staticid = id;
            }
            if (id == 0 || id == null)
            {
                id = staticid;
            }

            CategoryFill("main");
            if (id == null || con.Category == null)
            {
                return NotFound();
            }



            var category = await categoryRepository.Get(id);

            return View(category);


        }

        [HttpPost]
        public IActionResult CategoryEdit(Category category)
        {
            if (ModelState.IsValid)
            {
                bool answer = categoryRepository.Update(category);
                if (answer)
                {
                    TempData["Message"] = category.CategoryName + " Kategorisi Güncellendi";


                }
                else
                {
                    TempData["Message"] = "HATA";


                }
            }
            return RedirectToAction("CategoryIndex");
        }

        public async Task<IActionResult> CategoryDetails(int? id)
        {
            // Eğer id null değilse, staticid'yi güncelliyoruz

            if (id > 0)
            {
                staticid = id;
            }
            if (id == 0 || id == null)
            {
                id = staticid;
            }

            // Nullable int'i int'e dönüştürerek metodu çağırıyoruz
            ViewBag.ProductCount = productRepository.ProductCount(id, "CategoryName");  // id.Value ile int'e dönüştürüyoruz
            var category = await categoryRepository.Get(id);
            return View(category);
        }


        [HttpGet]
        public async Task<IActionResult> CategoryDelete(int? id)
        {
            if (id == null || con.Category == null)
            {
                return NotFound();
            }

            var category = await categoryRepository.Get(id);

            if (category == null)
            {

                return NotFound();
            }
            return View(category);

        }
        [HttpPost, ActionName("CategoryDelete")]
        public IActionResult CategoryDeleteConfirmed(int id)
        {
            bool answer = categoryRepository.Delete(id);
            if (answer)
            {
                TempData["Message"] = "Silindi";
                return RedirectToAction("CategoryIndex");

            }
            else
            {
                TempData["Message"] = "HATA";
                return RedirectToAction(nameof(CategoryDelete));
            }


        }


        public IActionResult SupplierIndex()
        {
            if (HttpContext.Session.GetString("Admin") != null)
            {
                List<Supplier> suppliers = supplierRepository.Get();
                return View(suppliers);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }

        [HttpGet]
        public IActionResult SupplierCreate()
        {
            if (HttpContext.Session.GetString("Admin") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public IActionResult SupplierCreate(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                bool answer = supplierRepository.Create(supplier);
                if (answer)
                {
                    TempData["Message"] = supplier.BrandName.ToUpper() + " Markası Eklendi";


                }
                else
                {
                    TempData["Message"] = "HATA";


                }
            }
            return RedirectToAction("SupplierCreate");

        }


        [HttpGet]
        public async Task<IActionResult> SupplierEdit(int? id)
        {
            if (id > 0)
            {
                staticid = id;
            }
            if (id == 0 || id == null)
            {
                id = staticid;
            }
            if (id == null || con.Supplier == null)
            {
                return NotFound();
            }
            var supplier = await supplierRepository.Get(id);
            return View(supplier);
        }
        [HttpPost]
        public IActionResult SupplierEdit(Supplier supplier)
        {

            if (ModelState.IsValid)
            {
                if (supplier.PhotoPath == null)
                {
                    supplier.PhotoPath = con.Supplier.FirstOrDefault
                    (s => s.SupplierID == supplier.SupplierID).PhotoPath.ToString();
                }
                bool answer = supplierRepository.Update(supplier);
                if (answer)
                {
                    TempData["Message"] = "Güncellendi";

                }
                else
                {
                    TempData["Message"] = "HATA";
                }
            }
            return RedirectToAction(nameof(SupplierEdit));

        }

        public async Task<IActionResult> SupplierDetails(int? id)
        {
            // Eğer id null değilse, staticid'yi güncelliyoruz

            if (id > 0)
            {
                staticid = id;
            }
            if (id == 0 || id == null)
            {
                id = staticid;
            }

            // Nullable int'i int'e dönüştürerek metodu çağırıyoruz

            var suplier = await supplierRepository.Get(id);
            ViewBag.ProductCount = productRepository.ProductCount(id, "BrandName");
            return View(suplier);
        }

        [HttpGet]
        public async Task<IActionResult> SupplierDelete(int? id)
        {
            if (id == null || con.Supplier == null)
            {
                return NotFound();
            }

            var supplier = await supplierRepository.Get(id);

            if (supplier == null)
            {

                return NotFound();
            }
            return View(supplier);

        }
        [HttpPost, ActionName("SupplierDelete")]
        public IActionResult SupplierDeleteConfirmed(int id)
        {
            bool answer = supplierRepository.Delete(id);
            if (answer)
            {
                TempData["Message"] = "Silindi";
                return RedirectToAction("SupplierIndex");

            }
            else
            {
                TempData["Message"] = "HATA";
                return RedirectToAction(nameof(SupplierIndex));
            }


        }

        [HttpGet]
        public IActionResult StatusIndex()
        {
            if (HttpContext.Session.GetString("Admin") != null)
            {
                List<Status> statuses = statusRepository.Get();
                return View(statuses);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }
        [HttpGet]
        public IActionResult StatusCreate()
        {
            if (HttpContext.Session.GetString("Admin") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public IActionResult StatusCreate(Status status)
        {
            if (ModelState.IsValid)
            {
                bool answer = statusRepository.Create(status);
                if (answer)
                {
                    TempData["Message"] = status.StatusName.ToUpper() + " Statüsü Eklendi";


                }
                else
                {
                    TempData["Message"] = "HATA";


                }
            }
            return RedirectToAction("StatusCreate");
        }
        [HttpGet]
        public async Task<IActionResult> StatusEdit(int? id)
        {


            if (id > 0)
            {
                staticid = id;
            }
            if (id == 0 || id == null)
            {
                id = staticid;
            }

            var status = await statusRepository.Get(id);
            return View(status);

        }
        [HttpPost]
        public IActionResult StatusEdit(Status status)
        {
            bool answer = statusRepository.Update(status);
            if (answer)
            {
                TempData["Message"] = "Güncellendi";
                return RedirectToAction(nameof(StatusIndex));
            }
            else
            {
                TempData["Message"] = "HATA";
                return RedirectToAction("StatusEdit");
            }
        }

        public async Task<IActionResult> StatusDetails(int? id)
        {
            // Eğer id null değilse, staticid'yi güncelliyoruz

            if (ModelState.IsValid)
            {
                if (id == null || con.Status == null)
                {
                    return NotFound();
                }
                var status = await statusRepository.Get(id);

                return View(status);
            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> StatusDelete(int? id)
        {
            if (id == null || con.Status == null)
            {
                return NotFound();
            }

            var status = await statusRepository.Get(id);

            if (status == null)
            {

                return NotFound();
            }
            return View(status);

        }
        [HttpPost, ActionName("StatusDelete")]
        public IActionResult StatusDeleteConfirmed(int id)
        {
            bool answer = statusRepository.Delete(id);
            if (answer)
            {
                TempData["Message"] = "Silindi";
                return RedirectToAction("StatusIndex");

            }
            else
            {
                TempData["Message"] = "HATA";
                return RedirectToAction(nameof(SupplierIndex));
            }


        }

        public IActionResult ProductIndex()
        {


            List<Product> products = productRepository.Get(0, "");
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> ProductCreate()
        {
            CategoryFill("all");
            SupplierFill();
            StatusFill();

            return View();

        }

        [HttpPost]
        public IActionResult ProductCreate(Product product)
        {
            if (ModelState.IsValid)
            {
                string answer = productRepository.Create(product);

                if (answer == "başarılı")
                {
                    TempData["Message"] = product.ProductName.ToUpper() + " ÜRÜN Eklendi";
                }

                else if (answer == "zaten kayıtlı")
                {

                    TempData["Message"] = "Ürün daha önce eklenmiş.";
                }
                else
                {

                    TempData["Message"] = "HATA";

                }

            }
            return RedirectToAction(nameof(ProductCreate));

        }

        [HttpGet]
        public async Task<IActionResult> ProductEdit(int? id)
        {
            CategoryFill("all");
            SupplierFill();
            StatusFill();



            if (id == null || con.Supplier == null)
            {
                return NotFound();
            }
            Product prd = await productRepository.Get(id);
            return View(prd);
        }
        [HttpPost]
        public IActionResult ProductEdit(Product product)
        {


            if (ModelState.IsValid)
            {

                if (product.PhotoPath == null)
                {
                    product.PhotoPath = con.Product.FirstOrDefault(p => p.ProductID == product.ProductID).PhotoPath;
                }

                bool answer = productRepository.Update(product);


                if (answer)
                {
                    TempData["Message"] = "Güncellendi";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["Message"] = "HATA";
                }
            }
            return RedirectToAction("ProductEdit");

        }

        [HttpGet]
        public async Task<IActionResult> ProductDelete(int? id)
        {
            if (id == null || con.Status == null)
            {
                return NotFound();
            }

            var product = await productRepository.Get(id);

            if (product == null)
            {

                return NotFound();
            }
            return View(product);

        }
        [HttpPost, ActionName("ProductDelete")]
        public IActionResult ProductDeleteConfirmed(int id)
        {
            bool answer = productRepository.Delete(id);
            if (answer)
            {
                TempData["Message"] = "Silindi";
                return RedirectToAction("ProductIndex");

            }
            else
            {
                TempData["Message"] = "HATA";
                return RedirectToAction(nameof(ProductIndex));
            }


        }

        public async Task<IActionResult> ProductDetails(int? id)
        {
            // Eğer id null değilse, staticid'yi güncelliyoruz

            if (ModelState.IsValid)
            {
                if (id == null || con.Product == null)
                {
                    return NotFound();
                }
                var product = await productRepository.Get(id);

                return View(product);
            }
            return View();

        }

        public async Task<IActionResult> UserSettings(User user)
        {

            var users = await userRepository.Get(); // Tüm kullanıcıları çekiyoruz
            return View(users); // View'a gönderiyoruz
        }
        [HttpGet]
        public async Task<IActionResult>UserEdit(int id)
        {
            User user = await userRepository.Get(id);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> UserEdit(User user)
        {
            if (ModelState.IsValid)
            {

               

                bool answer = userRepository.Update(user);


                if (answer)
                {
                    TempData["Message"] = "Güncellendi";
                    return RedirectToAction(nameof(UserSettings));
                }
                else
                {
                    TempData["Message"] = "HATA";
                }
            }
            return RedirectToAction("UserEdit");
        }

        public async Task<IActionResult> UserDelete(int id)
        {
            if (id == null || con.Status == null)
            {
                return NotFound();
            }

            var user = await userRepository.Get(id);

            if (user == null)
            {

                return NotFound();
            }
            return View(user);
        }
        [HttpPost, ActionName("UserDelete")]
        public IActionResult UserDeleteConfirmed(int id)
        {
            bool answer = userRepository.Delete(id);
            if (answer)
            {
                TempData["Message"] = "Silindi";
                return RedirectToAction("UserSettings");

            }
            else
            {
                TempData["Message"] = "HATA";
                return RedirectToAction(nameof(UserEdit));
            }


        }
        [HttpGet]
        public IActionResult AboutUsSettings()
        {
            var aboutUs = aboutUsRepository.Get();
            return View(aboutUs);
        }
        [HttpPost]
        public IActionResult AboutUsSettings(AboutUs model, IFormFile? PhotoPath)
        {
            if (ModelState.IsValid)
            {
                // Eğer yeni bir fotoğraf geldiyse, sunucuya kaydet
                if (PhotoPath != null && PhotoPath.Length > 0)
                {
                    // Dosya adını al
                    var fileName = Path.GetFileName(PhotoPath.FileName);

                    // wwwroot/img klasörüne kaydet
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        PhotoPath.CopyTo(stream);
                    }

                    model.PhotoPath = fileName; // Modelin içindeki PhotoPath alanına sadece dosya adını yaz
                }
                else
                {
                    // Eğer yeni fotoğraf yoksa, eski fotoğrafı koru
                    var existing = aboutUsRepository.Get();
                    model.PhotoPath = existing?.PhotoPath;
                }

                bool result = aboutUsRepository.Update(model);

                if (result)
                {
                    TempData["Message"] = "Güncelleme başarılı";
                }
                else
                {
                    TempData["Message"] = "Güncelleme başarısız";
                }

                return RedirectToAction(nameof(AboutUsSettings));
            }

            return View(model); // Hata varsa formu geri döndür
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Message"] = "Başarıyla çıkış yaptınız.";
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult CommunicationSettings()
        {
            var communication = communicationRepository.Get();
            return View(communication);

            
        }
        [HttpPost]
        public IActionResult CommunicationSettings(Communication communication)
        {
            bool answer = communicationRepository.Update(communication);
            if (answer)
            {
                TempData["Message"] = "Güncellendi";
               
            }
            else
            {
                TempData["Message"] = "HATA";
               
            }
            return RedirectToAction("CommunicationSettings");
        }

    }

    }


