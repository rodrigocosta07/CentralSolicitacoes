namespace SistemaEstoque.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterartabela3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovimentacoesConcluidas",
                c => new
                    {
                        Movimentacaoid = c.Int(nullable: false, identity: true),
                        idEquipamento = c.Int(nullable: false),
                        idSetor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Movimentacaoid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MovimentacoesConcluidas");
        }
    }
}
