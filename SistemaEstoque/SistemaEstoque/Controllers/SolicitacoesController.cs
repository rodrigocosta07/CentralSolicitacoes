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
using SistemaEstoque.Models.ViewModels;

namespace SistemaEstoque.Controllers
{
    public class SolicitacoesController : Controller
    {
        private EstoqueDbContext db = new EstoqueDbContext();

        // GET: Solicitacoes
        public async Task<ActionResult> Index()
        {
            return View(await db.Solicitacoes.ToListAsync());
        }

        // GET: Solicitacoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitacao solicitacao = await db.Solicitacoes.FindAsync(id);
            if (solicitacao == null)
            {
                return HttpNotFound();
            }
            return View(solicitacao);
        }

        // GET: Solicitacoes/Create
        public ActionResult Create()
        {
            ViewData["Setor"] = new SelectList(db.Setores.ToList(), "SetorId", "Nome");
            return View();
        }

        // POST: Solicitacoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SolicitacaoId,Equipamento,DataSolicitacao,Quantidade,Status")] SolicitacaoViewModel solicitacaoViewModel, int Setor)
        {

            var solicitacao = new Solicitacao
            {
                Equipamento = solicitacaoViewModel.Equipamento,
                Quantidade = solicitacaoViewModel.Quantidade,
                
            };

            if (ModelState.IsValid)
            {
                solicitacao.Status = Solicitacao.Estado.Aguardando;
                solicitacao.DataSolicitacao = DateTime.Now;
                db.Solicitacoes.Add(solicitacao);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(solicitacao);
        }


        public ActionResult GetSearchValue(string term)
        {
            return Json(db.Equipamentos.Where(c => c.NomeEquipamento.StartsWith(term)).Select(a => new { label = a.NomeEquipamento }), JsonRequestBehavior.AllowGet);
        }
        /*
        public JsonResult GetSearchValue(string search)
        {
            List<Equipamento> allsearch = db.Equipamentos.Where(x => x.NomeEquipamento.Contains(search)).Select(x => new Equipamento
            {
               EquipamentoId = x.EquipamentoId,
                NomeEquipamento = x.NomeEquipamento
            }).ToList();
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }*/

        // GET: Solicitacoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitacao solicitacao = await db.Solicitacoes.FindAsync(id);
            if (solicitacao == null)
            {
                return HttpNotFound();
            }
            return View(solicitacao);
        }

        // POST: Solicitacoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SolicitacaoId,Equipamento,DataSolicitacao,Quantidade,Status")] Solicitacao solicitacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitacao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(solicitacao);
        }

        // GET: Solicitacoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitacao solicitacao = await db.Solicitacoes.FindAsync(id);
            if (solicitacao == null)
            {
                return HttpNotFound();
            }
            return View(solicitacao);
        }

        // POST: Solicitacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Solicitacao solicitacao = await db.Solicitacoes.FindAsync(id);
            db.Solicitacoes.Remove(solicitacao);
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
