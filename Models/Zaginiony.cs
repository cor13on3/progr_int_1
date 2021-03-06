﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lost.Models
{
    public class Zaginiony
    {
        public int ZaginionyID { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Plec { get; set; }
        public string DataUrodzenia { get; set; }
        public byte[] Zdjecie { get; set; }
    }
}
