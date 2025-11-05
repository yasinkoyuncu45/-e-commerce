using MarketHup.Models.MVVM;

namespace MarketHup.Models
{
    public interface ICommunicationRepository
    {
         Communication Get();
         bool Update(Communication communication);
    }
}
    