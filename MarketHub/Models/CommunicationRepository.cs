using MarketHup.Models.MVVM;

namespace MarketHup.Models
{
    public class CommunicationRepository:ICommunicationRepository
    {
        public Communication Get()
        {
            Context con = new Context();

            Communication communication;
            communication = con.Communication.FirstOrDefault();


            return communication;

        }
        public bool Update(Communication communication)
        {
            using var con = new Context();

            try
            {
                // Aynı ID'ye sahip mevcut kayıt var mı?
                var existing = con.Communication.FirstOrDefault(c => c.ID == communication.ID);
                if (existing == null)
                    return false;

                // Alanları güncelle
                existing.Telephone = communication.Telephone;
                existing.Email = communication.Email;
                existing.Adress = communication.Adress;
                existing.WorkingHours = communication.WorkingHours;
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
