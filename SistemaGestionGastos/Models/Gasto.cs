using System;
using System.Collections.Generic;

namespace SistemaGestionGastos.Models
{
    public partial class Gasto
    {
        public int IdGasto { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdCategoria { get; set; }
        public decimal? Cantidad { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Descripcion { get; set; }

        public virtual CategoriasGasto? IdCategoriaNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
