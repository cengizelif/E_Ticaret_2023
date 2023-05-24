using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using E_Ticaret_2023.Models;
using Newtonsoft.Json;

namespace E_Ticaret_2023.Controllers
{
    [Authorize(Roles ="admin")]
    public class KategorilerController : Controller
    {
        private E_Ticaret_2023Entities db = new E_Ticaret_2023Entities();
        HttpClient client = new HttpClient();

        public ActionResult Index()
        {
            List<Kategoriler> liste = new List<Kategoriler>();

            client.BaseAddress=new Uri("https://localhost:44306/api/");
            var cevap=client.GetAsync("Kategori");
            cevap.Wait();

           if(cevap.Result.IsSuccessStatusCode)
            {
              var data=cevap.Result.Content.ReadAsStringAsync();
              data.Wait();
                liste = JsonConvert.DeserializeObject<List<Kategoriler>>(data.Result);
            }

            return View(liste);

            //return View(db.Kategoriler.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriler kategoriler = KategoriBul(id.Value);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

        private Kategoriler KategoriBul(int id)
        {
            Kategoriler kategori = null;

            //kategori=db.Kategoriler.Find(id);

            client.BaseAddress = new Uri("https://localhost:44306/api/");
            var cevap = client.GetAsync("Kategori/"+id.ToString());
            cevap.Wait();

            if(cevap.Result.IsSuccessStatusCode)
            {
               var data = cevap.Result.Content.ReadAsStringAsync();
                data.Wait();
               kategori= JsonConvert.DeserializeObject<Kategoriler>(data.Result);
            }

            return kategori;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kategoriler kategoriler)
        {
            if (ModelState.IsValid)
            {
                //db.Kategoriler.Add(kategoriler);
                //db.SaveChanges();
                //return RedirectToAction("Index");

                client.BaseAddress = new Uri("https://localhost:44306/api/");

               var cevap=client.PostAsJsonAsync<Kategoriler>("Kategori",kategoriler);
                cevap.Wait();
                if(cevap.Result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(kategoriler);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriler kategoriler = KategoriBul(id.Value);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Kategoriler kategoriler)
        {
            if (ModelState.IsValid)
            {
                kategoriler.KategoriAdi = kategoriler.KategoriAdi.Trim();
                client.BaseAddress = new Uri("https://localhost:44306/api/");
                var cevap = client.PutAsJsonAsync<Kategoriler>("Kategori", kategoriler);
                cevap.Wait();

                if(cevap.Result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                //db.Entry(kategoriler).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }
            return View(kategoriler);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriler kategoriler = KategoriBul(id.Value);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Kategoriler kategoriler = db.Kategoriler.Find(id);
            //db.Kategoriler.Remove(kategoriler);
            //db.SaveChanges();

            client.BaseAddress = new Uri("https://localhost:44306/api/");
            var cevap=client.DeleteAsync("Kategori/"+id.ToString());
            cevap.Wait();
            //if(cevap.Result.IsSuccessStatusCode)
            //{
            //    return RedirectToAction("Index");
            //}
            return RedirectToAction("Index");
        }

    }
}
