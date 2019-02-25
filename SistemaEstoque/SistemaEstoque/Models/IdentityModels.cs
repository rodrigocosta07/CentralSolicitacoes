using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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

        public virtual Setor Setor { get; set; }
        public ICollection<Solicitacao> Solicitacoes { get; set; }

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

        }

        public DbSet<Equipamento> Equipamentos { get; set; }
        public DbSet<Setor> Setores { get; set; }
        public DbSet<TipoEquipamento> TipoEquipamentos { get; set; }
        public DbSet<Solicitacao> Solicitacoes { get; set; }
        public DbSet<MovimentacoesConcluidas> Movimentacoes { get; set; }


    }
}