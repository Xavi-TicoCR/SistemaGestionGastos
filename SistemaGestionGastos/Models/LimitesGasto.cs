using System;
using System.Collections.Generic;

namespace SistemaGestionGastos.Models
{
    public partial class LimitesGasto
    {
        public int IdLimite { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdCategoria { get; set; }
        public decimal? MontoMaximo { get; set; }

        public virtual CategoriasGasto? IdCategoriaNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
