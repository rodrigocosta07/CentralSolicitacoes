using SistemaEstoque.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SistemaEstoque.Controllers
{
    public class ItensController : Controller
    {

        private EstoqueDbContext db = new EstoqueDbContext();

        
        public async Task<ActionResult> ListarItens(int? id)
        {
            var lista = await db.Produtoes.Where(x => x.ProdutoId == id).FirstOrDefaultAsync();
            ViewBag.Pedido = id;
            return PartialView(lista);
        }

        public async Task<ActionResult> SalvarItens(int Quantidade, string Produto, string Marca)
        {
            var item = new Produto()
            {
                Quantidade = Quantidade,
                NomeProduto = Produto,
                Marca = Marca,
                
            };
            item = await db.Produtoes.Where(x => x.NomeProduto.Equals(Produto)).FirstOrDefaultAsync();

            var test = item.ProdutoId;
            ViewBag.id = item.ProdutoId;
            return Json(new { Resultado = item.ProdutoId }, JsonRequestBehavior.AllowGet);
        }
    }
}