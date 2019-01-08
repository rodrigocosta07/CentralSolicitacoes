using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaEstoque.Models
{
    public class Relatorio
    {
        public int RelatorioId { get; set; }
        public DateTime DataVenda { get; set; }
        public double ValorLiquido { get; set; }
        public double ValorBruto { get; set; }
        public double Lucro { get; set; }

        public virtual ApplicationUser Usuario { get; set; }
    }
}