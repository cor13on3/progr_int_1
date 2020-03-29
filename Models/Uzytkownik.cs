using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lost.Models
{
    public class Uzytkownik
    {
        public string Email { get; set; }
        public string Haslo { get; set; }
    }
}
