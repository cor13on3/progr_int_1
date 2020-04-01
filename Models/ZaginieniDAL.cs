using Lost.Data;
using System.Collections.Generic;
using System.Linq;

namespace Lost.Models
{
    public class ZaginieniDAL
    {
        private LostContext _lostContext;

        public ZaginieniDAL(LostContext lostContext)
        {
            _lostContext = lostContext;
        }

        public IEnumerable<Zaginiony> GetOsoby(string plec = null)
        {
            return _lostContext.Zaginieni
                .Where(x => plec != null ? x.Plec == plec : x.Plec != null)
                .Select(x => new Zaginiony
                {
                    ZaginionyID = x.ZaginionyID,
                    Imie = x.Imie,
                    Nazwisko = x.Nazwisko,
                    Plec = x.Plec
                })
                .ToList();
        }

        public void DodajOsobe(Zaginiony zaginiony)
        {
            _lostContext.Zaginieni.Add(zaginiony);
            _lostContext.SaveChanges();
        }

        public void EdytujOsobe(Zaginiony zaginiony)
        {
            var res = _lostContext.Zaginieni.SingleOrDefault(x => x.ZaginionyID == zaginiony.ZaginionyID);
            if (res != null)
            {
                res.Imie = zaginiony.Imie;
                res.Nazwisko = zaginiony.Nazwisko;
                res.Plec = zaginiony.Plec;
                res.DataUrodzenia = zaginiony.DataUrodzenia;
                _lostContext.SaveChanges();
            }
        }

        public void UsunOsobe(int id)
        {
            var res = _lostContext.Zaginieni.SingleOrDefault(x => x.ZaginionyID == id);
            if (res != null)
            {
                _lostContext.Zaginieni.Remove(res);
                _lostContext.SaveChanges();
            }
        }

        public Zaginiony DajOsobe(int id)
        {
            var res = _lostContext.Zaginieni.SingleOrDefault(x => x.ZaginionyID == id);
            if (res != null)
                return res;
            return new Zaginiony();
        }

        public byte[] DajZdjecie(int id)
        {
            var res = _lostContext.Zaginieni.SingleOrDefault(x => x.ZaginionyID == id);
            if (res != null)
                return res.Zdjecie;
            return new byte[0];
        }
    }
}
