using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lost.Models
{
    public class ZaginieniDAL
    {
        private SqlConnection _sql;

        public ZaginieniDAL()
        {
            string conn = "Data Source=localhost;Initial Catalog=pi;Integrated Security=True";
            _sql = new SqlConnection(conn);
        }

        public IEnumerable<Zaginiony> GetOsoby()
        {
            var query = "select * from Zaginieni";
            SqlCommand command = new SqlCommand(query, _sql);
            _sql.Open();
            SqlDataReader reader = command.ExecuteReader();
            var osoby = new List<Zaginiony>();
            while (reader.Read())
            {
                osoby.Add(new Zaginiony
                {
                    Id = int.Parse(reader["id"].ToString()),
                    Imie = reader["imie"].ToString(),
                    Nazwisko = reader["nazwisko"].ToString(),
                    Plec = reader["plec"].ToString(),
                    DataUrodzenia = reader["dataur"].ToString()
                }); ; ;
            }
            _sql.Close();
            return osoby;
        }

        public void DodajOsobe(Zaginiony Zaginiony)
        {
            var query = $"insert into Zaginieni(imie,nazwisko,plec,dataur) " +
                $"values('{Zaginiony.Imie}'," +
                       $"'{Zaginiony.Nazwisko}'," +
                       $"'{Zaginiony.Plec}'," +
                       $"'{Zaginiony.DataUrodzenia}')";
            SqlCommand command = new SqlCommand(query, _sql);
            _sql.Open();
            command.ExecuteNonQuery();
            _sql.Close();
        }

        public void EdytujOsobe(Zaginiony Zaginiony)
        {
            var query = $"update Zaginieni set imie = '{Zaginiony.Imie}', nazwisko='{Zaginiony.Nazwisko}', plec='{Zaginiony.Plec}', dataur='{Zaginiony.DataUrodzenia}' where id={Zaginiony.Id}";
            SqlCommand command = new SqlCommand(query, _sql);
            _sql.Open();
            command.ExecuteNonQuery();
            _sql.Close();
        }

        public void UsunOsobe(int id)
        {
            var query = $"delete from Zaginieni where id={id}";
            SqlCommand command = new SqlCommand(query, _sql);
            _sql.Open();
            command.ExecuteNonQuery();
            _sql.Close();
        }

        public Zaginiony DajOsobe(int id)
        {
            var query = $"select * from Zaginieni where id={id}";
            SqlCommand command = new SqlCommand(query, _sql);
            _sql.Open();
            SqlDataReader reader = command.ExecuteReader();
            var Zaginiony = new Zaginiony { Id = id };
            while (reader.Read())
            {
                Zaginiony.Imie = reader["imie"].ToString();
                Zaginiony.Nazwisko = reader["nazwisko"].ToString();
                Zaginiony.Plec = reader["plec"].ToString();
                Zaginiony.DataUrodzenia = reader["dataur"].ToString();
            }
            _sql.Close();
            return Zaginiony;
        }
    }
}

