using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiKategoriler.Models;

namespace WebApiKategoriler.Controllers
{
    public class KategoriController : ApiController
    {
        private E_Ticaret_2023Entities db = new E_Ticaret_2023Entities();

        // GET: api/Kategori
        public IQueryable<Kategoriler> GetKategoriler()
        {
            return db.Kategoriler;
        }

        // GET: api/Kategori/5
        [ResponseType(typeof(Kategoriler))]
        public IHttpActionResult GetKategoriler(int id)
        {
            Kategoriler kategoriler = db.Kategoriler.Find(id);
            if (kategoriler == null)
            {
                return NotFound();
            }

            return Ok(kategoriler);
        }

        // PUT: api/Kategori/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKategoriler(int id, Kategoriler kategoriler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kategoriler.KategoriId)
            {
                return BadRequest();
            }

            db.Entry(kategoriler).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KategorilerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Kategori
        [ResponseType(typeof(Kategoriler))]
        public IHttpActionResult PostKategoriler(Kategoriler kategoriler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Kategoriler.Add(kategoriler);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kategoriler.KategoriId }, kategoriler);
        }

        // DELETE: api/Kategori/5
        [ResponseType(typeof(Kategoriler))]
        public IHttpActionResult DeleteKategoriler(int id)
        {
            Kategoriler kategoriler = db.Kategoriler.Find(id);
            if (kategoriler == null)
            {
                return NotFound();
            }

            db.Kategoriler.Remove(kategoriler);
            db.SaveChanges();

            return Ok(kategoriler);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KategorilerExists(int id)
        {
            return db.Kategoriler.Count(e => e.KategoriId == id) > 0;
        }
    }
}