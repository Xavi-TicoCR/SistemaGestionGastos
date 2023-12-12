using System;
using System.Collections.Generic;

namespace SistemaGestionGastos.Models
{
    public partial class Informe
    {
        public int IdInforme { get; set; }
        public int? IdUsuario { get; set; }
        public string? Periodo { get; set; }
        public decimal? TotalGastos { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
