using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaEstoque.Models;

namespace SistemaEstoque.Controllers
{
    public class Relatorios1Controller : Controller
    {
        private EstoqueDbContext db = new EstoqueDbContext();

        // GET: Relatorios1
        public async Task<ActionResult> Index()
        {
            return View(await db.Relatorios.ToListAsync());
        }

        public async Task<ActionResult> Caixa(string Pesquisa)
        {
            var pesq = Convert.ToInt64(Pesquisa);

            Produto produto = await db.Produtoes.Where(x => x.CodigoBarras.Equals(pesq)).FirstOrDefaultAsync();

            var listaPedido = new List<Produto>();
            
            return View(produto);
        }

        public async Task<ActionResult> Pedido(string Pesquisa)
        {
            var pesq = Convert.ToInt64(Pesquisa);
            
            Produto produto = await db.Produtoes.Where(x => x.CodigoBarras.Equals(pesq)).FirstOrDefaultAsync();
           
            return PartialView("Pedido",produto);
        }

        // GET: Relatorios1/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relatorio relatorio = await db.Relatorios.FindAsync(id);
            if (relatorio == null)
            {
                return HttpNotFound();
            }
            return View(relatorio);
        }

        // GET: Relatorios1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Relatorios1/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RelatorioId,DataVenda,ValorLiquido,ValorBruto,Lucro")] Relatorio relatorio)
        {
            if (ModelState.IsValid)
            {
                db.Relatorios.Add(relatorio);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(relatorio);
        }

        // GET: Relatorios1/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relatorio relatorio = await db.Relatorios.FindAsync(id);
            if (relatorio == null)
            {
                return HttpNotFound();
            }
            return View(relatorio);
        }

        // POST: Relatorios1/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RelatorioId,DataVenda,ValorLiquido,ValorBruto,Lucro")] Relatorio relatorio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(relatorio).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(relatorio);
        }

        // GET: Relatorios1/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relatorio relatorio = await db.Relatorios.FindAsync(id);
            if (relatorio == null)
            {
                return HttpNotFound();
            }
            return View(relatorio);
        }

        // POST: Relatorios1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Relatorio relatorio = await db.Relatorios.FindAsync(id);
            db.Relatorios.Remove(relatorio);
            await db.SaveChangesAsync();
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
