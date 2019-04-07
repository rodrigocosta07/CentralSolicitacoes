namespace SistemaEstoque.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NovasAlteracoes : DbMigration
    {
        public override void Up()
        {
            
            
            CreateTable(
                "dbo.MovimentacoesConcluidas",
                c => new
                    {
                        Movimentacaoid = c.Int(nullable: false, identity: true),
                        NomeEquipamento = c.String(),
                        idEquipamento = c.Int(nullable: false),
                        NomeSetor = c.String(),
                        idSetor = c.Int(nullable: false),
                        Quantidade = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Movimentacaoid);
            
            
            
        }
        
        public override void Down()
        {
           
            
           
        }
    }
}
