using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Comca_Internacional_3._0.Models;

namespace Comca_Internacional_3._0.Logica
{
    public class LO_Usuarios
    {
        public Usuarios EncontrarUsuario(string correo, string clave)
        {

            Usuarios objeto = new Usuarios();


            using (SqlConnection conexion = new SqlConnection("Data Source=comca.database.windows.net ;Initial Catalog=Comcam;Persist Security Info=True;User ID=mauricioc;Password=Crow2549762015"))
            {

                string query = "select Nombre,Correo,Clave,IdRol from USUARIOS where Correo = @pcorreo and Clave = @pclave";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@pcorreo", correo);
                cmd.Parameters.AddWithValue("@pclave", clave);
                cmd.CommandType = CommandType.Text;


                conexion.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {

                        objeto = new Usuarios()
                        {
                            Nombres = dr["Nombre"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Clave = dr["Clave"].ToString(),
                            IdRol = (Rol)dr["IdRol"],

                        };
                    }

                }
            }
            return objeto;

        }
    }
}