using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WpfVideoClub
{
    class ClanDal
    {
        private VideoKlub db = new VideoKlub();

        public List<Clan> VratiClanove()
        {
            return db.Clanovi.ToList();
        }

        public int UbaciClana(Clan c)
        {
            try
            {
                db.Clanovi.Add(c);
                db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                db.Entry(c).State = EntityState.Detached;
                return -1;
            }
        }

        public int PromeniClana(Clan c)
        {
            try
            {
                db.Entry(c).State = EntityState.Modified;
                db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                db.Entry(c).State = EntityState.Unchanged;
                return -1;
            }
        }

        public int ObrisiClana(Clan c)
        {
            try
            {
                db.Entry(c).State = EntityState.Deleted;
                db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                db.Entry(c).State = EntityState.Unchanged;
                return -1;
            }
        }
    }
}
