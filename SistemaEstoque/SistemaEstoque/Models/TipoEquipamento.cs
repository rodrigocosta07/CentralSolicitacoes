using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaEstoque.Models
{
    public class TipoEquipamento
    {
        public int TipoEquipamentoId { get; set; }
        public string NomeEquipamento { get; set; }

        public ICollection<Equipamento> Equipamentos { get; set; }
    }
}