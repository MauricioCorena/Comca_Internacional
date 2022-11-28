using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comca_Internacional_3._0.Models
{
    public class Usuarios
    {
        public string Nombres { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public Rol IdRol { get; set; }
    }
}