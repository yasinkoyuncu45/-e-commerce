using MarketHup.Models.MVVM;
using Microsoft.AspNetCore.Mvc;

namespace MarketHup.ViewComponents
{
    public class Contact : ViewComponent
    {
        Context con = new Context();
        public IViewComponentResult Invoke()
        {
            Communication? communication = con.Communication.FirstOrDefault();
            return View(communication); // 
        }
    }
}
