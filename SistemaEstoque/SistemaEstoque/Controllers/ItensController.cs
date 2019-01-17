using SistemaEstoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaEstoque.Controllers
{
    public class ItensController : Controller
    {

        private EstoqueDbContext db = new EstoqueDbContext();

        // GET: Itens
        public ActionResult ListarItens(int? id)
        {
            var lista = db.Produtoes.Where(x => x.ProdutoId == id);
            ViewBag.Pedido = id;
            return PartialView(lista);
        }

        public ActionResult SalvarItens(int Quantidade, string Produto, string Marca)
        {
            var item = new Produto()
            {
                Quantidade = Quantidade,
                NomeProduto = Produto,
                Marca = Marca,
                
            };
            item = db.Produtoes.Where(x => x.NomeProduto.Equals(Produto)).FirstOrDefault();

            var test = item.ProdutoId;
            ViewBag.id = item.ProdutoId;
            return Json(new { Resultado = item.ProdutoId }, JsonRequestBehavior.AllowGet);
        }
    }
}