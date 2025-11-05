using MarketHup.Models.MVVM;

namespace MarketHup.Models
{
    public interface IStatusRepository
    {
         List<Status> Get();
        
         Task<Status> Get(int? id);
         bool Create(Status status);
         bool Update(Status status);
         bool Delete(int id);

    }
}
