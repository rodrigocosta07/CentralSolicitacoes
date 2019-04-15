using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaEstoque.Models
{
    public class Equipamento
    {
        public int EquipamentoId { get; set; }
        [Display(Name = "Nome")]
        public string NomeEquipamento { get; set; }
        public string Marca { get; set; }
       
        public double Quantidade { get; set; }


        [Display(Name = "Tipo do Equipamento")]
        public virtual TipoEquipamento TipoEquipamento { get; set; }

        public virtual ICollection<Setor> Setor { get; set; }
    }
}