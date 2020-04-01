using Lost.Models;
using System.Linq;

namespace Lost.Data
{
    public static class DbInitializer
    {
        public static void Initialize(LostContext context)
        {
            context.Database.EnsureCreated();

            if (context.Uzytkownicy.Any())
                return;

            var uzytkownicy = new Uzytkownik[]
            {
                new Uzytkownik{Email = "a.malysz@wisla.pl", Haslo="janeahonen", Rola=RolaUzytkownika.Brak, Zbanowany = false},
                new Uzytkownik{Email = "admin@lost.pl", Haslo="admin", Rola=RolaUzytkownika.Moderator, Zbanowany = false},
            };

            foreach (var u in uzytkownicy)
                context.Uzytkownicy.Add(u);

            context.SaveChanges();

            var zaginieni = new Zaginiony[]
            {
                new Zaginiony{ Imie = "Andrzej", Nazwisko = "Buda", DataUrodzenia = "1960-01-01", Plec = "M"},
                new Zaginiony{ Imie = "Kinga", Nazwisko = "Buda", DataUrodzenia = "1985-01-01", Plec = "K"},
            };

            foreach (var z in zaginieni)
                context.Zaginieni.Add(z);

            context.SaveChanges();
        }
    }
}