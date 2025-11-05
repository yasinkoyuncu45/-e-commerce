using MarketHup.Models.MVVM;

namespace MarketHup.Models
{
    public class SupplierRepository: ISupplierRepository
    {
        Context con = new Context();
        public List<Supplier> Get()
        {

            List<Supplier> Supplier = con.Supplier.Where(s => s.Active == true).OrderBy(s => s.BrandName).ToList();
            return Supplier;
        }
        public async Task<Supplier> Get(int? id)
        {
            Supplier? supplier = await con.Supplier.FindAsync(id);
            return supplier;
        }
        public bool Create(Supplier supplier)
        {
            try
            {


                con.Add(supplier);
                con.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public  bool Update(Supplier supplier)
        {
            try
            {
                


                    con.Update(supplier);
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
                
                
                    Supplier? supplier = con.Supplier.FirstOrDefault(s => s.SupplierID == id);
                    supplier.Active = false;
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
