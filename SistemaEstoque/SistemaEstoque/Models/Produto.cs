using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaEstoque.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public string Marca { get; set; }
        public long CodigoBarras { get; set; }
        public double Quantidade { get; set; }
        public double ValorEntrada { get; set; }
        public double ValorVenda { get; set; }

        
        public virtual ApplicationUser Usuario { get; set; }
    }
}