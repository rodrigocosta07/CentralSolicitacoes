namespace SistemaEstoque.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NovoProjeto : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Produtoes", "Usuario_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Relatorios", "Usuario_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Produtoes", new[] { "Usuario_Id" });
            DropIndex("dbo.Relatorios", new[] { "Usuario_Id" });
            CreateTable(
                "dbo.Equipamento",
                c => new
                    {
                        EquipamentoId = c.Int(nullable: false, identity: true),
                        NomeEquipamento = c.String(),
                        Marca = c.String(),
                        NumeroSerie = c.Long(nullable: false),
                        Quantidade = c.Double(nullable: false),
                        Setor_SetorId = c.Int(),
                        TipoEquipamento_TipoEquipamentoId = c.Int(),
                    })
                .PrimaryKey(t => t.EquipamentoId)
                .ForeignKey("dbo.Setor", t => t.Setor_SetorId)
                .ForeignKey("dbo.TipoEquipamento", t => t.TipoEquipamento_TipoEquipamentoId)
                .Index(t => t.Setor_SetorId)
                .Index(t => t.TipoEquipamento_TipoEquipamentoId);
            
            CreateTable(
                "dbo.Setor",
                c => new
                    {
                        SetorId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.SetorId);
            
            CreateTable(
                "dbo.TipoEquipamento",
                c => new
                    {
                        TipoEquipamentoId = c.Int(nullable: false, identity: true),
                        NomeEquipamento = c.String(),
                    })
                .PrimaryKey(t => t.TipoEquipamentoId);
            
            CreateTable(
                "dbo.Solicitacao",
                c => new
                    {
                        SolicitacaoId = c.Int(nullable: false, identity: true),
                        Equipamento = c.String(),
                        DataSolicitacao = c.DateTime(nullable: false),
                        Quantidade = c.Double(nullable: false),
                        Status = c.Int(nullable: false),
                        Setor_SetorId = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SolicitacaoId)
                .ForeignKey("dbo.Setor", t => t.Setor_SetorId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.Setor_SetorId)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.AspNetUsers", "Setor_SetorId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Setor_SetorId");
            AddForeignKey("dbo.AspNetUsers", "Setor_SetorId", "dbo.Setor", "SetorId");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id");
            DropTable("dbo.Produtoes");
            DropTable("dbo.Relatorios");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Relatorios",
                c => new
                    {
                        RelatorioId = c.Int(nullable: false, identity: true),
                        DataVenda = c.DateTime(nullable: false),
                        ValorLiquido = c.Double(nullable: false),
                        ValorBruto = c.Double(nullable: false),
                        Lucro = c.Double(nullable: false),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RelatorioId);
            
            CreateTable(
                "dbo.Produtoes",
                c => new
                    {
                        ProdutoId = c.Int(nullable: false, identity: true),
                        NomeProduto = c.String(),
                        Marca = c.String(),
                        CodigoBarras = c.Long(nullable: false),
                        Quantidade = c.Double(nullable: false),
                        ValorEntrada = c.Double(nullable: false),
                        ValorVenda = c.Double(nullable: false),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProdutoId);
            
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Solicitacao", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Setor_SetorId", "dbo.Setor");
            DropForeignKey("dbo.Solicitacao", "Setor_SetorId", "dbo.Setor");
            DropForeignKey("dbo.Equipamento", "TipoEquipamento_TipoEquipamentoId", "dbo.TipoEquipamento");
            DropForeignKey("dbo.Equipamento", "Setor_SetorId", "dbo.Setor");
            DropIndex("dbo.AspNetUsers", new[] { "Setor_SetorId" });
            DropIndex("dbo.Solicitacao", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Solicitacao", new[] { "Setor_SetorId" });
            DropIndex("dbo.Equipamento", new[] { "TipoEquipamento_TipoEquipamentoId" });
            DropIndex("dbo.Equipamento", new[] { "Setor_SetorId" });
            DropColumn("dbo.AspNetUsers", "Setor_SetorId");
            DropTable("dbo.Solicitacao");
            DropTable("dbo.TipoEquipamento");
            DropTable("dbo.Setor");
            DropTable("dbo.Equipamento");
            CreateIndex("dbo.Relatorios", "Usuario_Id");
            CreateIndex("dbo.Produtoes", "Usuario_Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Relatorios", "Usuario_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Produtoes", "Usuario_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
