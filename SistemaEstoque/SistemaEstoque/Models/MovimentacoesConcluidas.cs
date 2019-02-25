using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaEstoque.Models
{
    public class MovimentacoesConcluidas
    {
        [Key]
        public int Movimentacaoid { get; set; }

        public int idEquipamento { get; set; }
        public int idSetor { get; set; }
        public double Quantidade { get; set; }
    }
}