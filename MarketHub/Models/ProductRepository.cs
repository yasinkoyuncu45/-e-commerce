using MarketHup.Models.MVVM;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MarketHup.Models
{
    public class ProductRepository:IProductRepository
    {
        Context con = new Context();
        public int page { get; set; }
        public int subpageCount { get; set; }
        public int pagenumber { get; set; }
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string? PhotoPath { get; set; }
        public string? Notes { get; set; }

        public  int ProductCount(int? id, string Name)
        {
            
                int Count = 0;
                if (Name == "CategoryName")
                {
                    Count = con.Product.Count(p => p.CategoryID == id);
                }
                else
                {
                    Count = con.Product.Count(p => p.SupplierID == id);
                }
                return Count;

            
        }
        public int TotalProduct(string value)
        {

            if (value == "New")
            {
                return con.Product.Count();
            }
            else
            {
                return con.Product.Where(p => p.StatusID == 2).Count();
            }





        }

        public List<Product> Get(int? ıd, string TableName)
        {
            List<Product> Product;
            if (TableName == "Category")
            {
                Product = con.Product.Where(p => p.CategoryID == ıd).ToList();
            }
            else if (TableName == "Supplier")
            {
                Product = con.Product.Where(p => p.SupplierID == ıd).Take(4).ToList();
            }

            else
            {
                Product = con.Product.ToList();
            }
            return Product;



        }
        public List<Product> Get(string Value, int pagenumber)
        {

            List<Product> Product;

            if (Value == "Slider")
            {

                Product = con.Product.Where(p => p.StatusID == 1)
               .ToList();

            }
            else if (Value == "New")
            {
                if (pagenumber == -1)
                {
                    Product = con.Product.OrderByDescending(p => p.AddDate).Take(8).ToList();
                }
                else if (pagenumber == 0)
                {
                    Product = con.Product.OrderByDescending(p => p.AddDate).Take(4).ToList();
                }
                else
                {
                    Product = con.Product.OrderByDescending(p => p.AddDate).Skip(4 * pagenumber).Take(4).ToList();
                }


            }
            else if (Value == "Special")
            {
                if (pagenumber == -1)
                {
                    Product = con.Product.Where(s => s.StatusID == 2).Take(8).ToList();
                }
                else if (pagenumber == 0)
                {
                    Product = con.Product.Where(s => s.StatusID == 2).Take(4).ToList();
                }
                else
                {
                    Product = con.Product.Where(s => s.StatusID == 2).Skip(4 * pagenumber).Take(4).ToList();
                }

            }

            else if (Value == "Discounted")
            {
                if (pagenumber == -1)
                {
                    Product = con.Product.OrderByDescending(p => p.Discount).Take(8).ToList();
                }
                else if (pagenumber == 0)
                {
                    Product = con.Product.OrderByDescending(p => p.Discount).Take(4).ToList();
                }
                else
                {
                    Product = con.Product.OrderByDescending(p => p.Discount).Skip(4 * pagenumber).Take(4).ToList();
                }
            }

            else if (Value == "Highlighted")
            {
                if (pagenumber == -1)
                {
                    Product = con.Product.OrderByDescending(p => p.HighLighted).Take(8).ToList();
                }
                else if (pagenumber == 0)
                {
                    Product = con.Product.OrderByDescending(p => p.HighLighted).Take(4).ToList();
                }
                else
                {
                    Product = con.Product.OrderByDescending(p => p.HighLighted).Skip(4 * pagenumber).Take(4).ToList();
                }

            }


            else if (Value == "Topseller")
            {

                Product = con.Product.OrderByDescending(p => p.TopSeller).Take(8).ToList();
            }
            else if (Value == "Star")
            {

                Product = con.Product.Where(p => p.StatusID == 3).Take(8).ToList();
            }
            else if (Value == "Opportunity")
            {

                Product = con.Product.Where(p => p.StatusID == 4).Take(8).ToList();
            }
            else if (Value == "NoTable")
            {

                Product = con.Product.Where(p => p.StatusID == 5).Take(8).ToList();
            }
            else if (Value == "Populer")
            {

                Product = con.Product.Where(p => p.StatusID == 7).Take(8).ToList();
            }

            else
            {
                Product = con.Product.ToList();

            }
            return Product;
        }
       
        public List<Product> Get(string Value, int id, int pagenumber)
        {
            int pageSize = 4;

            if (Value == "Category")
            {
                return con.Product
                         .Where(p => p.CategoryID == id)
                         .Skip(pagenumber * pageSize)
                         .Take(pageSize)
                         .ToList();
            }
            else
            {
                return con.Product
                    .Where(s => s.SupplierID == id)
                    .Skip(pagenumber * pageSize)
                    .Take(pageSize)
                    .ToList();
            }


        }
        public async Task<Product> Get(int? id)
        {
            Product? product = await con.Product.FirstOrDefaultAsync(c => c.ProductID == id);
            return product;
        }
        public Product Get(string Value)
        {

            return con.Product.FirstOrDefault(c => c.StatusID == 6);
        }
        public  string Create(Product product)
        {
            try
            {
                
                    Product product1 = con.Product.FirstOrDefault(p => p.ProductName.ToUpper() == product.ProductName.ToUpper() &&
                    p.SupplierID == product.SupplierID);

                    if (product1 == null)
                    {
                        product.AddDate = DateTime.Now;
                        con.Add(product);
                        con.SaveChanges();
                        return "başarılı";
                    }
                    else
                    {
                        return "zaten kayıtlı";
                    }


                
            }
            catch (Exception)
            {

                return "başarısız";
            }
        }
       
        public  bool Update(Product product)
        {
            try
            {
                

                    con.Update(product);
                    con.SaveChanges();

                    return true;
                
            }
            catch (Exception)
            {

                return false;
            }
        }
        public  bool Delete(int id)
        {
            try
            {
               
                    Product? product = con.Product.FirstOrDefault(p => p.ProductID == id);
                    product.Active = false;
                    con.SaveChanges();
                    return true;


               
            }
            catch (Exception)
            {

                return false;
            }
        }
    
        
        public PagedList<Product> TopsellerProductList()
        {
            PagedList<Product> model = new PagedList<Product>(con.Product.OrderByDescending(p => p.TopSeller), page, subpageCount);
            return model;
        }

        public  void Highlighted_Increase(int id)
        {
            
                Product? product = con.Product.FirstOrDefault(p => p.ProductID == id);
                product.HighLighted += 1;
                con.Update(product);
                con.SaveChanges();

            

        }

        public List<ProductRepository> SelectProductByDetails(string query)
        {
            List<ProductRepository> Product = new List<ProductRepository>();

            SqlConnection sqlConnection = Connection.ServerConnect;
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                ProductRepository product = new ProductRepository();
                product.ProductID = Convert.ToInt32(sqlDataReader["ProductID"]);
                product.ProductName = sqlDataReader["ProductName"].ToString();
                product.UnitPrice = Convert.ToDecimal(sqlDataReader["UnitPrice"]);
                product.PhotoPath = sqlDataReader["PhotoPath"].ToString();
                product.Notes = sqlDataReader["Notes"].ToString();
                Product.Add(product);
            }
            return Product;

        }

        public  List<Sp_Search> gettingSearchProduct(string id)
        {
            
                var Product = con.Sp_Searches.FromSqlRaw($"sp_arama {id}").ToList();
                return Product;
            
        }
    }
}
