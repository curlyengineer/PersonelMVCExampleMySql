﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonelMVC.Models
{
    public class personel
    {
        public int id { get; set; }
        public int Departmanid { get; set; }
        public  string Ad { get; set; }
        public string Soyad { get; set; }
        public int Yas { get; set; }
        public int Maas { get; set; }
        public string Dtarihi { get; set; }
        public string Cinsiyet { get; set; }
        public string  MedeniHal { get; set; }
    }
}