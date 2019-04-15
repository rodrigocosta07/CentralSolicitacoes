using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaEstoque.Models
{
    public class TipoEquipamento
    {
        public int TipoEquipamentoId { get; set; }
        [Display(Name = "Nome do Equipamento")]
        public string NomeEquipamento { get; set; }

        public ICollection<Equipamento> Equipamentos { get; set; }
    }
}