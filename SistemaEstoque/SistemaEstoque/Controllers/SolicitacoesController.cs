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
using Microsoft.AspNet.Identity;

namespace SistemaEstoque.Controllers
{
    [Authorize]
    public class SolicitacoesController : Controller
    {
        private EstoqueDbContext db = new EstoqueDbContext();

        // GET: Solicitacoes
        [Authorize(Roles = "AdminBens")]
        public async Task<ActionResult> Index()
        {
            var user = User.Identity.GetUserId();
            var DadosUsuario = db.Users.Find(user);

            ViewBag.quantidade = TempData["teste"];
            return View(await db.Solicitacoes.ToListAsync());
        }

        [Authorize(Roles ="Usuario , AdminBens")]
        public async Task<ActionResult> SolicitacoesSetor()
        {

            var user = User.Identity.GetUserId();
            var usuario = db.Users.Find(user);
            var Solicitacoes = await db.Solicitacoes.Where(x => x.Setor.SetorId.Equals(usuario.SetorId)).ToListAsync();
            return View(Solicitacoes);
        }

        [Authorize(Roles = "Usuario , AdminBens")]
        public async Task<ActionResult> EquipamentosSetor()
        {

            var user = User.Identity.GetUserId();
            var usuario = db.Users.Find(user);
            var Equipamentos = await db.Movimentacoes.Where(x => x.idSetor.Equals(usuario.SetorId)).ToListAsync();
            
            return View(Equipamentos);
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

        [Authorize(Roles = "AdminBens")]
        public ActionResult Aprovar(int? id)
        {

            var solicitacao = db.Solicitacoes.Find(id);

            MovimentacoesConcluidas movimentacao = new MovimentacoesConcluidas();
            var Equipamento = db.Equipamentos.Where(a => a.NomeEquipamento.Equals(solicitacao.Equipamento)).FirstOrDefault();
            Equipamento = db.Equipamentos.Find(Equipamento.EquipamentoId);
            var idequipamento = Equipamento.EquipamentoId;
            var setorid = solicitacao.Setor.SetorId;

           

            if(solicitacao.Quantidade > Equipamento.Quantidade)
            {
                ViewBag.ErroQuantidade = "Não há Equipamentos disponiveis para quantidade solicitada";
                
                TempData["teste"] = "Não há Equipamentos disponiveis para quantidade solicitada";
                return RedirectToAction("Index");
            }
            movimentacao.idSetor = setorid;
            movimentacao.NomeSetor = solicitacao.Setor.Nome;
            movimentacao.idEquipamento = idequipamento;
            movimentacao.NomeEquipamento = Equipamento.NomeEquipamento;
            movimentacao.Quantidade = solicitacao.Quantidade;

            solicitacao.Status = Solicitacao.Estado.Aprovada;

            Equipamento.Quantidade -= solicitacao.Quantidade;

            db.Entry(Equipamento).State = EntityState.Modified;
            db.SaveChanges();
            db.Entry(solicitacao).State = EntityState.Modified;
            db.SaveChanges();
            db.Movimentacoes.Add(movimentacao);
            db.SaveChanges();
            return RedirectToAction("Index", "Solicitacoes");

        }

        [Authorize(Roles = "AdminBens")]
        public ActionResult NegarSolicitacao(int? id)
        {
            var solicitacao = db.Solicitacoes.Find(id);
            solicitacao.Status = Solicitacao.Estado.Negada;

            db.Entry(solicitacao).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Solicitacoes");
        }


        // GET: Solicitacoes/Create
        [Authorize(Roles = "Usuario")]
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
        [Authorize(Roles = "Usuario")]
        public async Task<ActionResult> Create([Bind(Include = "SolicitacaoId,Equipamento,DataSolicitacao,Quantidade,Status")] SolicitacaoViewModel solicitacaoViewModel, int Setor)
        {

            var solicitacao = new Solicitacao
            {
                Equipamento = solicitacaoViewModel.Equipamento,
                Quantidade = solicitacaoViewModel.Quantidade,
                
            };

            if (ModelState.IsValid)
            {
                Setor setor = db.Setores.Find(Setor);

                solicitacao.Setor = setor;
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

        // GET: Solicitacoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitacao solicitacao =  db.Solicitacoes.Find(id);
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
        public ActionResult Edit(Solicitacao solicitacao)
        {


            if (ModelState.IsValid)
            {
                
                db.Entry(solicitacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(solicitacao);
        }
        /*
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
            ViewBag.setorid = solicitacao.Setor;
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
                var idEquipamento = db.Equipamentos.Where(a => a.NomeEquipamento.Equals(solicitacao.Equipamento)).FirstOrDefault();
                var teste = ViewBag.setorid;
                idEquipamento = db.Equipamentos.Find(idEquipamento.EquipamentoId);

                idEquipamento.Quantidade -= solicitacao.Quantidade;
                //solicitacao = await db.Solicitacoes.FindAsync(solicitacao.Setor.SetorId);
                //equipamento.Setor = solicitacao.Setor;

                db.Entry(idEquipamento).State = EntityState.Modified;
                db.Entry(solicitacao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(solicitacao);
        }
        */
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
