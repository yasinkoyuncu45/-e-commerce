using MarketHup.Models.MVVM;
using Microsoft.EntityFrameworkCore;

namespace MarketHup.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        Context con = new Context();

        public List<Category> Get(string value)
        {
            List<Category> categories;
            if (value == "all")
            {
                categories = con.Category.ToList();
            }
            else
            {
                categories = con.Category.Where(c => c.ParentID == 0).ToList();

            }
            return categories;

        }
        public async Task<Category> Get(int? id)
        {

            Category? category = await con.Category.FirstOrDefaultAsync(c => c.CategoryID == id);
            //Category? categories = await context.Categories.FindAsync(id);
            return category;
        }
        public  bool Create(Category category)
        {
            try
            {
                if (category.ParentID == null)
                {
                    category.ParentID = 0;
                }
                con.Add(category);
                con.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public  bool Update(Category category)
        {
            try
            {
                    if (category.ParentID == null)
                    {
                        category.ParentID = 0;
                    }

                    con.Update(category);
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
                    Category? category = con.Category.FirstOrDefault(c => c.CategoryID == id);
                    category.Active = false;

                    List<Category> categoryList = con.Category.Where(c => c.ParentID == id).ToList();
                    foreach (var item in categoryList)
                    {
                        item.Active = false;

                    }
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

