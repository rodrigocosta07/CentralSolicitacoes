using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaEstoque.Models.ViewModels
{
    public class SolicitacaoViewModel
    {
        public string Equipamento { get; set; }
        public double Quantidade { get; set; }
        public virtual Setor Setor { get; set; }
    }
}