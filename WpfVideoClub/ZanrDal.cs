using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WpfVideoClub
{
    class ZanrDal
    {
        private VideoKlub db = new VideoKlub();

        public List<Zanr> VratiZanrove()
        {
            return db.Zanrovi.ToList();
        }
    }
}
