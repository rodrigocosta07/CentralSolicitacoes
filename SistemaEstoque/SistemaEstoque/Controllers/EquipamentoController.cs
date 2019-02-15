using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaEstoque.Models;

namespace SistemaEstoque.Controllers
{
    public class EquipamentoController : Controller
    {
        private EstoqueDbContext db = new EstoqueDbContext();

        // GET: Equipamento
        public ActionResult Index()
        {
            return View(db.Equipamentos.ToList());
        }

        // GET: Equipamento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipamento equipamento = db.Equipamentos.Find(id);
            if (equipamento == null)
            {
                return HttpNotFound();
            }
            return View(equipamento);
        }

        // GET: Equipamento/Create
        public ActionResult Create()
        {
            ViewData["TipoEquipamento"] = new SelectList(db.TipoEquipamentos.ToList(), "TipoEquipamentoId", "NomeEquipamento");
            return View();
        }

        // POST: Equipamento/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EquipamentoId,NomeEquipamento,Marca,NumeroSerie,Quantidade")] Equipamento equipamento)
        {
            if (ModelState.IsValid)
            {
                db.Equipamentos.Add(equipamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipamento);
        }

        // GET: Equipamento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipamento equipamento = db.Equipamentos.Find(id);
            if (equipamento == null)
            {
                return HttpNotFound();
            }
            return View(equipamento);
        }

        // POST: Equipamento/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EquipamentoId,NomeEquipamento,Marca,NumeroSerie,Quantidade")] Equipamento equipamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equipamento);
        }

        // GET: Equipamento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipamento equipamento = db.Equipamentos.Find(id);
            if (equipamento == null)
            {
                return HttpNotFound();
            }
            return View(equipamento);
        }

        // POST: Equipamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipamento equipamento = db.Equipamentos.Find(id);
            db.Equipamentos.Remove(equipamento);
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
