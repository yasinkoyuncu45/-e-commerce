using MarketHup.Models.MVVM;

namespace MarketHup.Models
{
    public interface ICategoryRepository
    {
          List<Category> Get(string value);
          Task<Category> Get(int? id);
          bool Create(Category category);
          bool Update(Category category);
          bool Delete(int id);

    }
}
