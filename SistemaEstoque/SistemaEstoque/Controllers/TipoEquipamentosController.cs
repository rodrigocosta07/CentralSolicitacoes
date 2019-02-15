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
    public class TipoEquipamentosController : Controller
    {
        private EstoqueDbContext db = new EstoqueDbContext();

        // GET: TipoEquipamentos
        public ActionResult Index()
        {
            return View(db.TipoEquipamentos.ToList());
        }

        // GET: TipoEquipamentos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoEquipamento tipoEquipamento = db.TipoEquipamentos.Find(id);
            if (tipoEquipamento == null)
            {
                return HttpNotFound();
            }
            return View(tipoEquipamento);
        }

        // GET: TipoEquipamentos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoEquipamentos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoEquipamentoId,NomeEquipamento")] TipoEquipamento tipoEquipamento)
        {
            if (ModelState.IsValid)
            {
                db.TipoEquipamentos.Add(tipoEquipamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoEquipamento);
        }

        // GET: TipoEquipamentos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoEquipamento tipoEquipamento = db.TipoEquipamentos.Find(id);
            if (tipoEquipamento == null)
            {
                return HttpNotFound();
            }
            return View(tipoEquipamento);
        }

        // POST: TipoEquipamentos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoEquipamentoId,NomeEquipamento")] TipoEquipamento tipoEquipamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoEquipamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoEquipamento);
        }

        // GET: TipoEquipamentos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoEquipamento tipoEquipamento = db.TipoEquipamentos.Find(id);
            if (tipoEquipamento == null)
            {
                return HttpNotFound();
            }
            return View(tipoEquipamento);
        }

        // POST: TipoEquipamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoEquipamento tipoEquipamento = db.TipoEquipamentos.Find(id);
            db.TipoEquipamentos.Remove(tipoEquipamento);
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
