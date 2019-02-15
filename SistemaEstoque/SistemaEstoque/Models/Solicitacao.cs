using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaEstoque.Models
{
    public class Solicitacao
    {
        public enum Estado
        {
            Aguardando = 0,
            Negada = 1,
            Aprovada = 2

        }

        public int SolicitacaoId { get; set; }
        public string Equipamento { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public double Quantidade { get; set; }

        public virtual Setor Setor { get; set; }
        public virtual Estado Status {get;set;}
    }
}