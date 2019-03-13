namespace SistemaEstoque.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class altertabelauser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Setor_SetorId", "dbo.Setor");
            DropIndex("dbo.AspNetUsers", new[] { "Setor_SetorId" });
            AddColumn("dbo.AspNetUsers", "SetorId", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "Setor_SetorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Setor_SetorId", c => c.Int());
            DropColumn("dbo.AspNetUsers", "SetorId");
            CreateIndex("dbo.AspNetUsers", "Setor_SetorId");
            AddForeignKey("dbo.AspNetUsers", "Setor_SetorId", "dbo.Setor", "SetorId");
        }
    }
}
