using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using System.Web.Security;
using Comca_Internacional_3._0.Models;
using Comca_Internacional_3._0.Permisos;


namespace Comca_Internacional_3._0.Controllers
{
  
    public class HomeController : Controller
    {
        //[Authorize (Roles= "Administrador, Tecnologia, Recursos_Humanos")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [PermisosRol(Rol.Administrador)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        [PermisosRol(Rol.Administrador)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize]
        public ActionResult SinPermiso()
        {
            ViewBag.Message = "Usted no cuenta con permisos para ver esta pagina";

            return View();
        }
        [Authorize]
        public ActionResult CerrarSesion()
        {

            FormsAuthentication.SignOut();
            Session["Usuario"] = null;


            return RedirectToAction("Index", "Acceso");
        }

        [Authorize]
        [PermisosRol(Rol.Administrador)]
        public ActionResult Contabilidad()
        {
            return View();  
        }

        [Authorize]
        [PermisosRol(Rol.Administrador)]
        public ActionResult Recursos_Humanos()
        {

            return View();
        }

        [Authorize]
        [PermisosRol(Rol.Administrador)]
        public ActionResult RegistarEmple()
        {
           
            return View();
        }

        [AllowAnonymous]
        public ActionResult Asistencia()
        {
            Asistencia Asis = new Asistencia();
            return View(Asis);
        }

        [HttpGet]
        public JsonResult ListaAsistencia()
        {
            List<Asistencia> ListaA = new List<Asistencia>();

            //Conexion con la base de datos
            using (SqlConnection con = new SqlConnection("Data Source=comca.database.windows.net ;Initial Catalog=Comcam;Persist Security Info=True;User ID=mauricioc;Password=Crow2549762015"))
            {

                try
                {

                    SqlCommand cmd = new SqlCommand("sp_mostrar_lista_asis ", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader da1 = cmd.ExecuteReader();
                    while (da1.Read())
                    {
                        ListaA.Add(new Asistencia()
                        {
                            IdAsistencia = da1["Id Asistencia"].ToString(),
                            DUI = da1["DUI"].ToString(),
                            Nombre = da1["nombre"].ToString(),
                            fecha = da1["fecha"].ToString(),
                            HoraEntrada = da1["Hora Entrada"].ToString(),
                            HoraSalida = da1["Hora Salida"].ToString(),
                            Rutina = da1["Rutina"].ToString(),
                        });
                    }

                }
                catch (Exception)
                {

                    throw;
                }
                return Json(new { data = ListaA }, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Asistencia(Asistencia Asis)
        {
            
            if (ModelState.IsValid)
            {
                ProcesosEmpleados proc2 = new ProcesosEmpleados();
                Asistencia Registro = new Asistencia()
                {
                    DUI = Asis.DUI,
                    fecha = Asis.fecha,
                    Rutina = Asis.Rutina
                };
                //llama el metodo agregar Asistencia
                proc2.AgregarAsistencia(Registro);
                return View(Asis);
            }



            //invocamos la vista
            return View(Asis);

        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult AsistenciasDiarias()
        {
            SqlCommand comando = null;
            List<Asistencia> ListaDiaia = new List<Asistencia>();


            //Conexion con la base de datos
            using (SqlConnection con = new SqlConnection("Data Source=comca.database.windows.net ;Initial Catalog=Comcam;Persist Security Info=True;User ID=mauricioc;Password=Crow2549762015"))
            {

                try
                {

                    con.Open();

                    Asistencia mostrar = new Asistencia();
                    //Consultando el registro ingresado y mostrando en pantalla
                    comando = new SqlCommand("select CONCAT(e.Nombre,' ', e.Apellidos) as " +
                        "Nombre, a.fecha , a.[Hora Entrada], a.[Hora Salida] from " +
                        "Asistencia a join Empleados  e on a.DUI = e.DUI " +
                        "where  a.fecha =  CONVERT(date, GETDATE())", con);
                    comando.CommandType = System.Data.CommandType.Text;

                    SqlDataReader da1 = comando.ExecuteReader();
                    while (da1.Read())
                    {
                        ListaDiaia.Add(new Asistencia()
                        {
                            Nombre = da1["nombre"].ToString(),
                            HoraEntrada = da1["Hora Entrada"].ToString(),
                            HoraSalida = da1["Hora Salida"].ToString(),
                        });
                    }

                }
                catch (Exception)
                {

                    throw;
                }
                return Json(new { data = ListaDiaia }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult MostrarSueldo(Planilla p1)
        {

            List<Planilla> ListaA = new List<Planilla>();

            //Conexion con la base de datos
            using (SqlConnection con = new SqlConnection("Data Source=comca.database.windows.net ;Initial Catalog=Comcam;Persist Security Info=True;User ID=mauricioc;Password=Crow2549762015"))
            {

                try
                {

                    SqlCommand cmd = new SqlCommand("ps_mostrar_sueldo ", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader da1 = cmd.ExecuteReader();
                    while (da1.Read())
                    {
                        double Suel = Convert.ToDouble(da1["Sueldo"].ToString());
                        double ValorISS = Suel * 0.03;
                        double ValorAFP = Suel * 0.0725;

                        //Calculos de tabla para la renta
                        double tramo1 = 472.00;
                        double tramo2 = 895.24;
                        double tramo3 = 2038.10;

                        double couta2 = 17.67;
                        double couta3 = 60.00;
                        double couta4 = 288.57;

                        double exceso1 = 472.00;
                        double exceso2 = 895.24;
                        double exceso3 = 2038.10;
                        /// fin de tabla 

                        double SalarioafpISS = Suel - ValorISS - ValorAFP;
                        double Rtramo4 = ((SalarioafpISS - exceso3) * 0.30) + couta4;

                        double ValorRenta = SalarioafpISS <= tramo1 ? 0.00 : SalarioafpISS <= tramo2 ? ((SalarioafpISS - exceso1) * 0.10) + couta2 : SalarioafpISS <= tramo3 ? ((SalarioafpISS - exceso2) * 0.20) + couta3 : Rtramo4;
                        //Fin calculo d ela Renta

                        /// Totales//
                        double TotalDescuentos = ValorAFP + ValorISS + ValorRenta;
                        double TotalPagar = Suel - TotalDescuentos;


                        //calculo de dias laborales 


                        Planilla pla1 = new Planilla()
                        {
                            ISSS = Convert.ToDecimal(ValorISS),
                            AFP = Convert.ToDecimal(ValorAFP),
                            Renta = Convert.ToDecimal(ValorRenta),
                            TotalDescuento = Convert.ToDecimal(TotalDescuentos),
                            TotalPago = Convert.ToDecimal(TotalPagar),

                        };

                        ListaA.Add(new Planilla()
                        {
                            Nombre = da1["nombre"].ToString(),
                            DUI = da1["DUI"].ToString(),
                            FechaInicio = da1["fecha_final"].ToString(),
                            FechaCorte = da1["fecha_final"].ToString(),
                            Sueldo = Convert.ToDecimal(da1["Sueldo"].ToString()),
                            Dias = Convert.ToInt32(da1["Dias_laboral"].ToString()),
                            ISSS = pla1.ISSS,
                            AFP = pla1.AFP,
                            Renta = pla1.Renta,
                            TotalDescuento = pla1.TotalDescuento,
                            TotalPago = pla1.TotalPago,

                        });

                    }

                }
                catch (Exception)
                {

                    throw;
                }
                return Json(new { data = ListaA }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult ListaEmpleados()
        {
            List<Empleados> Lista = new List<Empleados>();
            //Conexion con la base de datos
            using (SqlConnection con = new SqlConnection("Data Source=comca.database.windows.net ;Initial Catalog=Comcam;Persist Security Info=True;User ID=mauricioc;Password=Crow2549762015"))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("sp_mostrar_emple", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader da = cmd.ExecuteReader();
                    while (da.Read())
                    {
                        Lista.Add(new Empleados()
                        {
                            DUI = da["DUI"].ToString(),
                            Nombre = da["Nombre"].ToString(),
                            Apellidos = da["Apellidos"].ToString(),
                            Direccion = da["Direccion"].ToString(),
                            Telefono = da["Telefono"].ToString(),
                            IdTipoEmp = da["IdTipoEmp"].ToString(),
                            FechaNacimiento = da["Fecha Nacimiento"].ToString(),
                            Puesto = da["Puesto"].ToString(),
                            FechaContratacion = da["Fecha Contratacion"].ToString(),
                            Sueldo = Convert.ToInt32(da["Sueldo"])
                        });
                    }

                }
                catch (Exception)
                {

                    throw;
                }
                return Json(new { data = Lista }, JsonRequestBehavior.AllowGet);
            }
        }
 
        [HttpPost]
        public ActionResult RegistarEmple(Empleados Empl)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    ProcesosEmpleados proc1 = new ProcesosEmpleados();

                    //Creando el objeto empleados
                    Empleados Registro = new Empleados()
                    {

                        DUI = Empl.DUI,
                        Nombre = Empl.Nombre,
                        Apellidos = Empl.Apellidos,
                        Direccion = Empl.Direccion,
                        Telefono = Empl.Telefono,
                        IdTipoEmp = Empl.IdTipoEmp,
                        FechaNacimiento = Empl.FechaNacimiento,
                        Puesto = Empl.Puesto,
                        FechaContratacion = Empl.FechaContratacion,
                        Sueldo = Empl.Sueldo
                    };

                    //Llamando el metetodo Agregar
                    proc1.AgregarEmpl(Registro);
                    return View();
                }

            }
            catch (Exception)
            {

                throw;
            }

            //invocamos la vista
            return View(Empl);
        }
    }
}