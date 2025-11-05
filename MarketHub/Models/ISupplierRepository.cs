using MarketHup.Models.MVVM;

namespace MarketHup.Models
{
    public interface ISupplierRepository
    {
         List<Supplier> Get();
         Task<Supplier> Get(int? id);
         bool Create(Supplier supplier);
         bool Update(Supplier supplier);
         bool Delete(int id);


    }
}
