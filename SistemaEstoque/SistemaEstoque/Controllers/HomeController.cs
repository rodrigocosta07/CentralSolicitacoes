using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaEstoque.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole("Usuario"))
            {
               return RedirectToAction("SolicitacoesSetor", "Solicitacoes");
            }else if (User.IsInRole("AdminBens"))
            {
                return RedirectToAction("Index", "Solicitacoes");
            }else {
                return RedirectToAction("Index" , "Role");
            }
            
        }

       
    }
}