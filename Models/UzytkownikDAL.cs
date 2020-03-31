using System.Collections.Generic;
using System.Data.SqlClient;

namespace Lost.Models
{
    public class UzytkownikDAL
    {
        private SqlConnection _sql;

        public UzytkownikDAL()
        {
            string conn = "Data Source=localhost;Initial Catalog=pi;Integrated Security=True";
            _sql = new SqlConnection(conn);
        }

        public void Dodaj(Uzytkownik uzytkownik)
        {
            var query = $"insert into Uzytkownicy values('{uzytkownik.Email}', '{uzytkownik.Haslo}', 0, 0)";
            SqlCommand command = new SqlCommand(query, _sql);
            _sql.Open();
            command.ExecuteNonQuery();
            _sql.Close();
        }

        public Uzytkownik Daj(string email)
        {
            var query = $"select * from Uzytkownicy where email='{email}'";
            SqlCommand command = new SqlCommand(query, _sql);
            _sql.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                return new Uzytkownik
                {
                    Email = reader["email"].ToString(),
                    Haslo = reader["haslo"].ToString(),
                    Rola = (RolaUzytkownika)reader["rola"],
                    Zbanowany = (bool)reader["zbanowany"]
                };
            }
            return null;
        }

        public Uzytkownik[] Przegladaj()
        {
            var query = $"select email, rola, zbanowany from Uzytkownicy";
            SqlCommand command = new SqlCommand(query, _sql);
            _sql.Open();
            SqlDataReader reader = command.ExecuteReader();
            var wynik = new List<Uzytkownik>();
            while (reader.Read())
            {
                wynik.Add(new Uzytkownik
                {
                    Email = reader["email"].ToString(),
                    Rola = (RolaUzytkownika)reader["rola"],
                    Zbanowany = (bool)reader["zbanowany"]
                });
            }
            return wynik.ToArray();
        }

        public void Banuj(string email, bool value)
        {
            int val = value ? 1 : 0;
            var query = $"update Uzytkownicy set zbanowany={val.ToString()} where email='{email}'";
            SqlCommand command = new SqlCommand(query, _sql);
            _sql.Open();
            command.ExecuteNonQuery();
            _sql.Close();
        }
    }
}

