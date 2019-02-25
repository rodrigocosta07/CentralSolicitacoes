namespace SistemaEstoque.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterartabela : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Equipamento", "NumeroSerie");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Equipamento", "NumeroSerie", c => c.Long(nullable: false));
        }
    }
}
