using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WpfVideoClub
{
    class FilmDal
    {
        private VideoKlub db = new VideoKlub();

        public List<Film> VratiFilmove()
        {
            return db.Filmovi.ToList();
        } 

        public int UbaciFilm(Film f)
        {
            try
            {
                db.Filmovi.Add(f);
                db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                db.Entry(f).State = EntityState.Detached;
                return -1;
            }
        }

        public int PromeniFilm(Film f)
        {
            try
            {
                db.Entry(f).State = EntityState.Modified;
                db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                db.Entry(f).State = EntityState.Unchanged;

                return -1;
            }
        }

        public int ObrisiFilm(Film f)
        {
            try
            {
                db.Entry(f).State = EntityState.Deleted;
                db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                db.Entry(f).State = EntityState.Unchanged;
                return -1;
            }
        }
    }
}
