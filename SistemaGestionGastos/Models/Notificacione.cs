using System;
using System.Collections.Generic;

namespace SistemaGestionGastos.Models
{
    public partial class Notificacione
    {
        public int IdNotificacion { get; set; }
        public int? IdUsuario { get; set; }
        public string? Mensaje { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Estado { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
