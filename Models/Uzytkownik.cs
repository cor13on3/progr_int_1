using System.ComponentModel.DataAnnotations.Schema;

namespace Lost.Models
{
    public class Uzytkownik
    {
        public int UzytkownikID { get; set; }
        public string Email { get; set; }
        public string Haslo { get; set; }
        public RolaUzytkownika Rola { get; set; }
        public bool Zbanowany { get; set; }
    }

    public enum RolaUzytkownika
    {
        Brak,
        Moderator
    }
}
