using MarketHup.Models.MVVM;
using System.Drawing;

namespace MarketHup.Models
{
    public class AboutUsRepository: IAboutUsRepository
    {

        public AboutUs Get()
        {
            Context con = new Context();

            AboutUs aboutUs;
            aboutUs = con.AboutUs.FirstOrDefault();
            
            
            return aboutUs;

        }
        
        public bool Update(AboutUs aboutUs)
        {
            Context con = new Context();

            try
            {


                con.Update(aboutUs);
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
