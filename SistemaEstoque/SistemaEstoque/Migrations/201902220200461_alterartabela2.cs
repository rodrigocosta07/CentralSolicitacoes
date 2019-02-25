namespace SistemaEstoque.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterartabela2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Equipamento", "Setor_SetorId", "dbo.Setor");
            DropIndex("dbo.Equipamento", new[] { "Setor_SetorId" });
            CreateTable(
                "dbo.SetorEquipamento",
                c => new
                    {
                        Setor_SetorId = c.Int(nullable: false),
                        Equipamento_EquipamentoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Setor_SetorId, t.Equipamento_EquipamentoId })
                .ForeignKey("dbo.Setor", t => t.Setor_SetorId)
                .ForeignKey("dbo.Equipamento", t => t.Equipamento_EquipamentoId)
                .Index(t => t.Setor_SetorId)
                .Index(t => t.Equipamento_EquipamentoId);
            
            DropColumn("dbo.Equipamento", "Setor_SetorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Equipamento", "Setor_SetorId", c => c.Int());
            DropForeignKey("dbo.SetorEquipamento", "Equipamento_EquipamentoId", "dbo.Equipamento");
            DropForeignKey("dbo.SetorEquipamento", "Setor_SetorId", "dbo.Setor");
            DropIndex("dbo.SetorEquipamento", new[] { "Equipamento_EquipamentoId" });
            DropIndex("dbo.SetorEquipamento", new[] { "Setor_SetorId" });
            DropTable("dbo.SetorEquipamento");
            CreateIndex("dbo.Equipamento", "Setor_SetorId");
            AddForeignKey("dbo.Equipamento", "Setor_SetorId", "dbo.Setor", "SetorId");
        }
    }
}
