using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using MySql.Data.MySqlClient;
using PersonelMVC.Controllers;
using PersonelMVC.Models;

namespace PersonelMVC.Controllers
{
    public class HomeController : Controller
    {
        MySqlConnection baglanti = new MySqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["bcum"].ConnectionString);

        // GET: Home
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Departman()
        {
            var departman = new List<PersonelMVC.Models.Departman>();
            baglanti.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from departman", baglanti);
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                departman.Add(new PersonelMVC.Models.Departman
                {
                    id = Convert.ToInt32(rd["id"]),
                    Departman_kodu = rd["Departman_kodu"].ToString(),
                    Name = rd["Name"].ToString()
                });
            }
            baglanti.Close();
            return View(departman);

        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("DepartmanForm");
        }
        [HttpPost]
        public ActionResult Kaydet(Departman departman)
        {
            if (departman.id == 0)
            {
                baglanti.Open();
                MySqlCommand cmd = new MySqlCommand("Insert into departman (id,Departman_kodu,Name)values (Null,'" + @departman.Departman_kodu + "','" + departman.Name + "')", baglanti);
                cmd.ExecuteNonQuery();
                baglanti.Close();
            }
            else
            {
                baglanti.Open();
                MySqlCommand cmd = new MySqlCommand("Update departman set Departman_kodu='" + @departman.Departman_kodu + "',Name='" + @departman.Name + "' where id=" + @departman.id + "", baglanti);
                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    departman.Departman_kodu = rd["Departman_kodu"].ToString();
                    departman.Name = rd["Name"].ToString();
                }
                baglanti.Close();
            }

            return RedirectToAction("Departman", "Home");
        }
        public ActionResult DepartmanEdit(Departman departman)
        {
            // var departman = new List<PersonelMVC.Models.Departman>();

            baglanti.Open();
            MySqlCommand cmd = new MySqlCommand("Select Departman_kodu,Name from departman where id=" + @departman.id + "", baglanti);
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                departman.Departman_kodu = rd["Departman_kodu"].ToString();
                departman.Name = rd["Name"].ToString();
            }
            baglanti.Close();
            return View("DepartmanForm", departman);
        }
        public ActionResult DepartmanDelete(int id)
        {
            baglanti.Open();
            MySqlCommand cmd = new MySqlCommand("Delete from departman where id='" + id + "'", baglanti);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            return RedirectToAction("Departman", "Home");
        }
        public ActionResult Personel()
        {
            var personel = new List<PersonelMVC.Models.personel>();
            baglanti.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from personel", baglanti);
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                personel.Add(new PersonelMVC.Models.personel
                {
                    id = Convert.ToInt32(rd["id"]),
                    Departmanid = Convert.ToInt32(rd["Departmanid"]),
                    Ad = rd["Ad"].ToString(),
                    Soyad = rd["Soyad"].ToString()
                }) ;
            }
            baglanti.Close();
            return View(personel);
        }
        public ActionResult PersonelDetails(int id)
        {
            var personel = new List<PersonelMVC.Models.personel>();
            baglanti.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from personel where id="+id+"", baglanti);
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                personel.Add(new PersonelMVC.Models.personel
                {
                    Departmanid = Convert.ToInt32(rd["Departmanid"]),
                    Ad = rd["Ad"].ToString(),
                    Soyad = rd["Soyad"].ToString(),
                    Yas = Convert.ToInt32(rd["Yas"]),
                    Maas = Convert.ToInt32(rd["Maas"]),
                    Dtarihi = rd["Dtarihi"].ToString(),
                    Cinsiyet = rd["Cinsiyet"].ToString(),
                    MedeniHal = rd["MedeniHal"].ToString()
                });
            }
            baglanti.Close();
            return View(personel);
        }
        public ActionResult PersonelDelete(int id)
        {
            baglanti.Open();
            MySqlCommand cmd = new MySqlCommand("Delete from personel where id='" + id + "'", baglanti);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            return RedirectToAction("Personel", "Home");
        }
        [HttpGet]
        public ActionResult PersonelCreate()
        {
            return View("PersonelForm");
        }
        [HttpPost]
        public ActionResult PersonelKaydet(personel personel)
        {
            if (@personel.id == 0) { 
            baglanti.Open();
            string query = "Insert into personel(Departmanid,Ad,Soyad,Yas,Maas,Dtarihi,Cinsiyet,MedeniHal) values"+
           " (" +@personel.Departmanid + ",'" +@personel.Ad + "','" +@personel.Soyad + "',"+@personel.Yas + "," +@personel.Maas + ",'" +@personel.Dtarihi + "','" +@personel.Cinsiyet + "','" +@personel.MedeniHal + "')";
            MySqlCommand cmd = new MySqlCommand(query, baglanti);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            }else
            {
                baglanti.Open();
                MySqlCommand cmd = new MySqlCommand("Update personel set Departmanid=" +@personel.Departmanid + ",Ad='" +personel.Ad+ "',"+
                "Soyad='"+personel.Soyad+"',Yas="+@personel.Yas+",Maas="+personel.Maas+",Dtarihi='"+personel.Dtarihi+"'"+
                " , Cinsiyet='"+personel.Cinsiyet+"', MedeniHal='"+personel.MedeniHal+"' where id=" + personel.id + "", baglanti);
                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    personel.Departmanid = Convert.ToInt32(rd["Departmanid"]);
                    personel.Ad = rd["Ad"].ToString();
                    personel.Soyad = rd["Soyad"].ToString();
                    personel.Yas = Convert.ToInt32(rd["Yas"]);
                    personel.Maas = Convert.ToInt32(rd["Maas"]);
                    personel.Dtarihi = rd["Dtarihi"].ToString();
                    personel.Cinsiyet = rd["Cinsiyet"].ToString();
                    personel.MedeniHal = rd["MedeniHal"].ToString();
                }
                baglanti.Close();
            }
            return RedirectToAction("Personel", "Home");
        }
        public ActionResult PersonelEdit(personel Personel)
        {
            baglanti.Open();
            MySqlCommand cmd = new MySqlCommand("Select id,Departmanid,Ad,Soyad,Yas,Maas,Dtarihi,Cinsiyet,MedeniHal from personel where id=" + Personel.id + "", baglanti);
            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Personel.Departmanid = Convert.ToInt32(rd["Departmanid"]);
                Personel.Ad = rd["Ad"].ToString();
                Personel.Soyad = rd["Soyad"].ToString();
                Personel.Yas = Convert.ToInt32(rd["Yas"]);
                Personel.Maas = Convert.ToInt32(rd["Maas"]);
                Personel.Dtarihi = rd["Dtarihi"].ToString();
                Personel.Cinsiyet = rd["Cinsiyet"].ToString();
                Personel.MedeniHal = rd["MedeniHal"].ToString();
                Personel.id = int.Parse(rd["id"].ToString());
            }
            baglanti.Close();
            return View("PersonelForm", Personel);
        }
       
    }
}
