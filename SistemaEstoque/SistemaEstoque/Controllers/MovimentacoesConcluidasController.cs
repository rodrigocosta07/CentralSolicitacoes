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
    public class MovimentacoesConcluidasController : Controller
    {
        private EstoqueDbContext db = new EstoqueDbContext();

        // GET: MovimentacoesConcluidas
        public async Task<ActionResult> Index()
        {
            return View(await db.Movimentacoes.ToListAsync());
        }

        // GET: MovimentacoesConcluidas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimentacoesConcluidas movimentacoesConcluidas = await db.Movimentacoes.FindAsync(id);
            if (movimentacoesConcluidas == null)
            {
                return HttpNotFound();
            }
            return View(movimentacoesConcluidas);
        }

        // GET: MovimentacoesConcluidas/Create
        public ActionResult Create()
        {
            return View();
        }
        /*
        public ActionResult Salvar(int id)
        {

            return View();
        }
        */

        [HttpPost]
        public ActionResult Aprovar(int? id)
        {
           
            var solicitacao = db.Solicitacoes.Find(id);

            MovimentacoesConcluidas movimentacao = new MovimentacoesConcluidas();
            var idEquipamento = db.Equipamentos.Where(a => a.NomeEquipamento.Equals(solicitacao.Equipamento)).FirstOrDefault();
            idEquipamento = db.Equipamentos.Find(idEquipamento.EquipamentoId);
            var idequipe = idEquipamento.EquipamentoId;

            var setorid = solicitacao.Setor.SetorId;

            movimentacao.idSetor = setorid;
            movimentacao.idEquipamento = idequipe;
            db.Movimentacoes.Add(movimentacao);
            db.SaveChanges();
            return RedirectToAction("Index", "Solicitacoes");
             
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Salvar(int? id)
        {
           
        }



        // POST: MovimentacoesConcluidas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Movimentacaoid,idEquipamento,idSetor")] MovimentacoesConcluidas movimentacoesConcluidas)
        {
            if (ModelState.IsValid)
            {
                db.Movimentacoes.Add(movimentacoesConcluidas);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(movimentacoesConcluidas);
        }

        // GET: MovimentacoesConcluidas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimentacoesConcluidas movimentacoesConcluidas = await db.Movimentacoes.FindAsync(id);
            if (movimentacoesConcluidas == null)
            {
                return HttpNotFound();
            }
            return View(movimentacoesConcluidas);
        }

        // POST: MovimentacoesConcluidas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Movimentacaoid,idEquipamento,idSetor")] MovimentacoesConcluidas movimentacoesConcluidas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movimentacoesConcluidas).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(movimentacoesConcluidas);
        }

        // GET: MovimentacoesConcluidas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimentacoesConcluidas movimentacoesConcluidas = await db.Movimentacoes.FindAsync(id);
            if (movimentacoesConcluidas == null)
            {
                return HttpNotFound();
            }
            return View(movimentacoesConcluidas);
        }

        // POST: MovimentacoesConcluidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MovimentacoesConcluidas movimentacoesConcluidas = await db.Movimentacoes.FindAsync(id);
            db.Movimentacoes.Remove(movimentacoesConcluidas);
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
