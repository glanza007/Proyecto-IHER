﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProyectoIHER.Models;

namespace ProyectoIHER.Controllers
{
    public class PreguntasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Preguntas
        public ActionResult Index()
        {
            return View(db.Preguntas.ToList());
        }

        // GET: Preguntas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Preguntas preguntas = db.Preguntas.Find(id);
            if (preguntas == null)
            {
                return HttpNotFound();
            }
            return View(preguntas);
        }

        // GET: Preguntas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Preguntas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PreguntaID,NombrePregunta,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion")] Preguntas preguntas)
        {
            if (ModelState.IsValid)
            {
                preguntas.CreadoPor = User.Identity.GetUserName();
                preguntas.FechaCreacion = DateTime.Now;
                preguntas.ModificadoPor = User.Identity.GetUserName();
                preguntas.FechaModificacion = DateTime.Now;
                db.Preguntas.Add(preguntas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(preguntas);
        }

        // GET: Preguntas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Preguntas preguntas = db.Preguntas.Find(id);
            if (preguntas == null)
            {
                return HttpNotFound();
            }
            return View(preguntas);
        }

        // POST: Preguntas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PreguntaID,NombrePregunta,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion")] Preguntas preguntas)
        {
            if (ModelState.IsValid)
            {
                preguntas.CreadoPor = User.Identity.GetUserName();
                preguntas.FechaCreacion = DateTime.Now;
                preguntas.ModificadoPor = User.Identity.GetUserName();
                preguntas.FechaModificacion = DateTime.Now;
                db.Entry(preguntas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(preguntas);
        }

        // GET: Preguntas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Preguntas preguntas = db.Preguntas.Find(id);
            if (preguntas == null)
            {
                return HttpNotFound();
            }
            return View(preguntas);
        }

        // POST: Preguntas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Preguntas preguntas = db.Preguntas.Find(id);
            db.Preguntas.Remove(preguntas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
