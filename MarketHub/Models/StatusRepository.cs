using MarketHup.Models.MVVM;
using Microsoft.EntityFrameworkCore;

namespace MarketHup.Models
{
    public class StatusRepository:IStatusRepository
    {
        Context con = new Context();
        public List<Status> Get()
        {

            return con.Status.ToList();

        }
        public async Task<Status> Get(int? id)
        {
            return await con.Status.FirstOrDefaultAsync(s => s.StatusID == id);

        }
        public bool Create(Status status)
        {
            try
            {


                con.Add(status);
                con.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public  bool Update(Status status)
        {
            try
            {
                


                    con.Update(status);
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
               
                    Status? status = con.Status.FirstOrDefault(s => s.StatusID == id);
                    status.Active = false;
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
