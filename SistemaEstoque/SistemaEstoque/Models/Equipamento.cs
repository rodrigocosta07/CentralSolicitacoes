using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaEstoque.Models
{
    public class Equipamento
    {
        public int EquipamentoId { get; set; }
        public string NomeEquipamento { get; set; }
        public string Marca { get; set; }
       
        public double Quantidade { get; set; }

     

        public virtual TipoEquipamento TipoEquipamento { get; set; }

        public virtual ICollection<Setor> Setor { get; set; }
    }
}