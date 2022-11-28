
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using Comca_Internacional_3._0.Models;
using Comca_Internacional_3._0.Logica;
using System.Web.Security;


namespace Comca_Internacional_3._0.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            Usuarios objeto = new LO_Usuarios().EncontrarUsuario(correo, clave);

            if (objeto.Nombres != null)
            {


                FormsAuthentication.SetAuthCookie(objeto.Correo, false);

                Session["Usuario"] = objeto;

                return RedirectToAction("Index", "Home");
            }



            return View();
        }
    }
}