using Lost.Data;
using System.Linq;

namespace Lost.Models
{
    public class UzytkownikDAL
    {
        private LostContext _lostContext;

        public UzytkownikDAL(LostContext lostContext)
        {
            _lostContext = lostContext;
        }

        public void Dodaj(Uzytkownik uzytkownik)
        {
            _lostContext.Uzytkownicy.Add(uzytkownik);
            _lostContext.SaveChanges();
        }

        public Uzytkownik Daj(string email)
        {
            var res = _lostContext.Uzytkownicy.SingleOrDefault(x => x.Email == email);
            if (res != null)
                return res;
            return new Uzytkownik();
        }

        public Uzytkownik[] Przegladaj()
        {
            return _lostContext.Uzytkownicy.ToArray();
        }

        public void Banuj(string email, bool value)
        {
            var res = _lostContext.Uzytkownicy.SingleOrDefault(x => x.Email == email);
            if (res != null)
            {
                res.Zbanowany = value;
                _lostContext.SaveChanges();
            }
        }
    }
}

