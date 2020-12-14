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
    public class PersonalDataController : Controller
    {
        private Entities db = new Entities();

        [HttpPost]
        public JsonResult Exists([Bind(Include = "id,imie,nazwisko,pesel,nr_telefonu,email")] dane_personalne dane_personalne)
        {
            if (TempData["pesel"] != null)
            {
                if (dane_personalne.pesel == TempData["pesel"].ToString())
                    return Json(true);
                else
                    return Json(this.db.dane_personalne.Where(d => d.pesel == dane_personalne.pesel).FirstOrDefault() == null);
            }
            else
                return Json(this.db.dane_personalne.Where(d => d.pesel == dane_personalne.pesel).FirstOrDefault() == null);
        }
    }
}
