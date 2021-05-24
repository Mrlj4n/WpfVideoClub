using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace WpfVideoClub
{
    class IznajmljivanjeDal
    {
        private VideoKlub db = new VideoKlub();

        public List<ViewIznajmljivanja> VratiIznajmljivanja()
        {
            using (VideoKlub db1 = new VideoKlub())
            {
                return db1.ViewIznajmljivanja.ToList();
            }
        }

        public Iznajmljivanje VratiIznajmljivanje(int id)
        {
            return db.Iznajmljivanja.Find(id);
        }

        public int UbaciIznajmljivanje(Iznajmljivanje i)
        {
            try
            {
                db.Iznajmljivanja.Add(i);
                db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                db.Entry(i).State = EntityState.Detached;
                return -1;
            }
        }

        public int PromeniIznajmljivanje(Iznajmljivanje i)
        {
            try
            {
                db.Entry(i).State = EntityState.Modified;
                db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                db.Entry(i).State = EntityState.Unchanged;
                return -1;
            }
        }

        public int ObrisiIznajmljivanje(Iznajmljivanje i)
        {
            try
            {
                db.Entry(i).State = EntityState.Deleted;
                db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                db.Entry(i).State = EntityState.Unchanged;
                return -1;
            }
        }
    }
}
