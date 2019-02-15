using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaEstoque.Models
{
    public class Setor
    {
        public int SetorId { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Equipamento> Equipamentos { get; set; }
    }
}