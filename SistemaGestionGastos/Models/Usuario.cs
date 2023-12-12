using System;
using System.Collections.Generic;

namespace SistemaGestionGastos.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Administradores = new HashSet<Administradore>();
            Gastos = new HashSet<Gasto>();
            Informes = new HashSet<Informe>();
            LimitesGastos = new HashSet<LimitesGasto>();
            Notificaciones = new HashSet<Notificacione>();
        }

        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? Contraseña { get; set; }
        public DateTime? FechaCreacion { get; set; }

        public virtual ICollection<Administradore> Administradores { get; set; }
        public virtual ICollection<Gasto> Gastos { get; set; }
        public virtual ICollection<Informe> Informes { get; set; }
        public virtual ICollection<LimitesGasto> LimitesGastos { get; set; }
        public virtual ICollection<Notificacione> Notificaciones { get; set; }
    }
}
