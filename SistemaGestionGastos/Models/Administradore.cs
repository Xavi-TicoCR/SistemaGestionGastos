using System;
using System.Collections.Generic;

namespace SistemaGestionGastos.Models
{
    public partial class Administradore
    {
        public int IdAdministrador { get; set; }
        public int? IdUsuario { get; set; }
        public string? Rol { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
