using MarketHup.Models.MVVM;

namespace MarketHup.Models
{
    public interface IAboutUsRepository
    {
        public AboutUs Get();
        public bool Update(AboutUs aboutUs);
    }
}
