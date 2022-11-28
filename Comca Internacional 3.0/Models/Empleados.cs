using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Comca_Internacional_3._0.Models
{
    public class Empleados
    {
        [Required(ErrorMessage = "Campo Obligatorio")]
        [Display(Name = "DUI:")]
        public string DUI { get; set; }
        [Required]
        [Display(Name = "Nombres:")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Apellidos:")]
        public string Apellidos { get; set; }
        [Required]
        [Display(Name = "Direccion:")]
        public string Direccion { get; set; }
        [Required]
        [Display(Name = "Telefono:")]
        public string Telefono { get; set; }
        [Required]
        [Display(Name = "Estado:")]
        public string IdTipoEmp { get; set; }
        [Required]
        [Display(Name = "Fecha de Nacimiento:")]
        public string FechaNacimiento { get; set; }
        [Required]
        [Display(Name = "Puesto:")]
        public string Puesto { get; set; }
        [Required]
        [Display(Name = "Fecha Contratacion:")]
        public string FechaContratacion { get; set; }
        [Required]
        [Display(Name = "Sueldo:")]
        public float Sueldo { get; set; }
        public string bono { get; set; }
        public string fecha { get; set; }
        public double CantidadBono { get; set; }
    }

    public class Asistencia
    {
        public string IdAsistencia { get; set; }
        [Required(ErrorMessage = "Campo Obligatorio")]
        [StringLength(10, ErrorMessage = "Ingrese Dui correctamente 00000000-0")]
        [MinLength(9, ErrorMessage = "Ingrese Dui correctamente 00000000-0")]
        [Display(Name = "Ingrese DUI:")]
        public string DUI { get; set; }
        public string Nombre { get; set; }
        [Display(Name = "Fecha:")]
        [Required]
        public string fecha { get; set; }
        public string HoraEntrada { get; set; }
        public string HoraSalida { get; set; }
        public string SumaUnidad { get; set; }
        [Required]
        [Display(Name = "Rutina:")]
        public string Rutina { get; set; }
    }

    public class Incapacidades
    {
        public string Id_incapacidades { get; set; }
        public string DUI { get; set; }
        public string Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Hora_In { get; set; }
        public string Hora_Fin { get; set; }
        public string Unidad_H { get; set; }

    }

    public class Planilla
    {
        public string Nombre { get; set; }
        public string DUI { get; set; }
        [Display(Name = "Fecha inicio:")]
        public string FechaInicio { get; set; }
        public string FechaCorte { get; set; }
        public decimal Sueldo { get; set; }
        public int Dias { get; set; }
        public decimal Bono { get; set; }

        public decimal Renta { get; set; }
        public decimal AFP { get; set; }
        public decimal ISSS { get; set; }
        public decimal TotalDescuento { get; set; }
        public decimal TotalPago { get; set; }
        public decimal fecha { get; set; }


    }

    public class Usuario
    {
        public int idUser { get; set; }
        [Display(Name = "Ingrese Usuario")]
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string NombreU { get; set; }
        [Display(Name = "Ingrese Contraseña")]
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Password { get; set; }
        public int IdRol { get; set; }
        public string DUI { get; set; }
    }

    public class RolPermisos
    {
        public int Id_rol_permisos { get; set; }
        public int Idroles { get; set; }
        public int Idpermisos { get; set; }
    }


}