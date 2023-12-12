using System;
using System.Collections.Generic;

namespace SistemaGestionGastos.Models
{
    public partial class CategoriasGasto
    {
        public CategoriasGasto()
        {
            Gastos = new HashSet<Gasto>();
            LimitesGastos = new HashSet<LimitesGasto>();
        }

        public int IdCategoria { get; set; }
        public string? NombreCategoria { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<Gasto> Gastos { get; set; }
        public virtual ICollection<LimitesGasto> LimitesGastos { get; set; }
    }
}
