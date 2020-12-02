using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using firma_budowlana.Models;


namespace firma_budowlana.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            Entities db = new Entities();

            ViewBag.Message = "O firmie.";

            int[] amounts = { db.klienci.Count(), db.zlecenia.Count(), db.maszyny.Count(), db.pracownicy.Count() };

            ViewBag.amounts = amounts;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Kontakt.";

            return View();
        }

        public ActionResult Tables()
        {
            ViewBag.Message = "Tabele";

            return View();
        }
    }
}