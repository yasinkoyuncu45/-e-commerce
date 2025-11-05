using MarketHup.Models.MVVM;
using Microsoft.AspNetCore.Mvc;

namespace MarketHup.ViewComponents
{
    public class Headers:ViewComponent
    {
       
            Context con = new Context();

            public IViewComponentResult Invoke()
            {

                List<Category> categories = con.Category.Where(c => c.Active).ToList();
                return View(categories);
            }

        
    }
}
