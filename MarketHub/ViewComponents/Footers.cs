using MarketHup.Models.MVVM;
using Microsoft.AspNetCore.Mvc;

namespace MarketHup.ViewComponents
{
    public class Footers:ViewComponent
    {
        Context con = new Context();

        public IViewComponentResult Invoke()
        {

            List<Supplier> suppliers = con.Supplier.Where(s => s.Active).ToList();
            return View(suppliers);
        }
    }
}
