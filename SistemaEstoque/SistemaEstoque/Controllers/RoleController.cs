using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaEstoque.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SistemaEstoque.Controllers
{
    public class RoleController : Controller
    {

        private EstoqueDbContext db = new EstoqueDbContext();
        // GET: Role
        public ActionResult Index()
        {
            var Roles = db.Roles.ToList();
            return View(Roles);
        }

        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }
        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            db.Roles.Add(Role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}