using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SistemaEstoque.Models
{
    // É possível adicionar dados do perfil do usuário adicionando mais propriedades na sua classe ApplicationUser, visite https://go.microsoft.com/fwlink/?LinkID=317594 para obter mais informações.
    public class ApplicationUser : IdentityUser
    {

        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        public virtual ICollection<Produto> Produto { get; set; }
        public virtual ICollection<Relatorio> Relatorio { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Observe que o authenticationType deve corresponder àquele definido em CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Adicionar declarações de usuário personalizado aqui
            return userIdentity;
        }
    }

    public class EstoqueDbContext : IdentityDbContext<ApplicationUser>
    {
        public EstoqueDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static EstoqueDbContext Create()
        {
            return new EstoqueDbContext();
        }

        public System.Data.Entity.DbSet<SistemaEstoque.Models.Relatorio> Relatorios { get; set; }

        public System.Data.Entity.DbSet<SistemaEstoque.Models.Produto> Produtoes { get; set; }
    }
}