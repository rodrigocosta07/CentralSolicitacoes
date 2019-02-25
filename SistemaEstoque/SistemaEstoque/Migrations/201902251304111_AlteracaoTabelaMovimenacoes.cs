namespace SistemaEstoque.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoTabelaMovimenacoes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovimentacoesConcluidas", "Quantidade", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MovimentacoesConcluidas", "Quantidade");
        }
    }
}
