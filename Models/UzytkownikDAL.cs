using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
            var query = $"insert into Uzytkownicy values('{uzytkownik.Email}', '{uzytkownik.Haslo}')";
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
                };
            }
            return null;
        }
    }
}

