using MarketHup.Models.MVVM;
using X.PagedList;

namespace MarketHup.Models
{
    public interface IProductRepository
    {
              int ProductCount(int? id, string Name);
              int TotalProduct(string value);
              List<Product> Get(int? ıd, string TableName);
              List<Product> Get(string Value, int pagenumber);
              List<Product> Get(string Value, int id, int pagenumber);
              Task<Product> Get(int? id);
              Product Get(string Value);
              string Create(Product product);
              bool Update(Product product);
              bool Delete(int id);
              PagedList<Product> TopsellerProductList();
              void Highlighted_Increase(int id);
              List<ProductRepository> SelectProductByDetails(string query);
              List<Sp_Search> gettingSearchProduct(string id);
    }
}
