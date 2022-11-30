using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Windows;

namespace Comca_Internacional_3._0.Models
{
    public class ProcesosEmpleados
    {
        public int AgregarEmpl(Empleados EmplRes)
        {


            //Conexion con la base de datos
            //Conexion con la base de datos
            using (SqlConnection con = new SqlConnection("Data Source=comca.database.windows.net ;Initial Catalog=Comcam;Persist Security Info=True;User ID=mauricioc;Password=Crow2549762015"))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("sp_registro_empleados", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //definimos los tipos y valos parametrizados
                cmd.Parameters.AddWithValue("@DUI", EmplRes.DUI);
                cmd.Parameters.AddWithValue("@Nombre", EmplRes.Nombre);
                cmd.Parameters.AddWithValue("@Apellidos", EmplRes.Apellidos);
                cmd.Parameters.AddWithValue("@Direccion", EmplRes.Direccion);
                cmd.Parameters.AddWithValue("@Telefono", EmplRes.Telefono);
                cmd.Parameters.AddWithValue("@IdTipoEmp", EmplRes.IdTipoEmp);
                cmd.Parameters.AddWithValue("@FechadeNacimiento", EmplRes.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Puesto", EmplRes.Puesto);
                cmd.Parameters.AddWithValue("@FechaContratacion", EmplRes.FechaContratacion);
                cmd.Parameters.AddWithValue("@Sueldo", EmplRes.Sueldo);
                int i = cmd.ExecuteNonQuery();
                con.Close();

                return i;

            }
        }

        //public int ModificarEmpl(Empleados EmplRes)
        //{
        //    //Conexion con la base de datos
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sql"].ConnectionString);
        //    con.Open();

        //    SqlCommand cmd = new SqlCommand("sp_update_empleados", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    //definimos los tipos y valos parametrizados
        //    cmd.Parameters.AddWithValue("@DUI", EmplRes.DUI);
        //    cmd.Parameters.AddWithValue("@Nombre", EmplRes.Nombre);
        //    cmd.Parameters.AddWithValue("@Apellidos", EmplRes.Apellidos);
        //    cmd.Parameters.AddWithValue("@Direccion", EmplRes.Direccion);
        //    cmd.Parameters.AddWithValue("@Telefono", EmplRes.Telefono);
        //    cmd.Parameters.AddWithValue("@IdTipoEmp", EmplRes.IdTipoEmp);
        //    cmd.Parameters.AddWithValue("@FechadeNacimiento", EmplRes.FechaNacimiento);
        //    cmd.Parameters.AddWithValue("@Puesto", EmplRes.Puesto);
        //    cmd.Parameters.AddWithValue("@FechaContratacion", EmplRes.FechaContratacion);
        //    cmd.Parameters.AddWithValue("@Sueldo", EmplRes.Sueldo);
        //    int i = cmd.ExecuteNonQuery();
        //    con.Close();

        //    return i;


        //}
        public int AgregarAsistencia(Asistencia PerAsis)
        {
            int i = 0;
            //Conexion con la base de datos
            using (SqlConnection con = new SqlConnection("Data Source=comca.database.windows.net ;Initial Catalog=Comcam;Persist Security Info=True;User ID=mauricioc;Password=Crow2549762015"))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_asit_pers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //definimos los tipos y valos parametrizados
                    cmd.Parameters.AddWithValue("@DUI", PerAsis.DUI);
                    cmd.Parameters.AddWithValue("@fecha", PerAsis.fecha);
                    cmd.Parameters.AddWithValue("@Rutina", PerAsis.Rutina);

                    i = cmd.ExecuteNonQuery();

                    con.Close();
                    return i;
                }
                catch (Exception ex)
                {
                   
                    return i;
                }

              
            }
        }

        public int AgregarIncapacidad(Incapacidades PerInca)
        {
            //Conexion con la base de datos
            using (SqlConnection con = new SqlConnection("Data Source=comca.database.windows.net ;Initial Catalog=Comcam;Persist Security Info=True;User ID=mauricioc;Password=Crow2549762015"))
            {    
                con.Open();

            SqlCommand cmd = new SqlCommand("sp_registro_incapacidad", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //definimos los tipos y valos parametrizados
            cmd.Parameters.AddWithValue("@DUI", PerInca.DUI);
            cmd.Parameters.AddWithValue("@fecha", PerInca.Fecha);
            cmd.Parameters.AddWithValue("@Descripcion", PerInca.Descripcion);
            cmd.Parameters.AddWithValue("@HoraIn", PerInca.Hora_In);
            cmd.Parameters.AddWithValue("@HoraFin", PerInca.Hora_Fin);
            cmd.Parameters.AddWithValue("@UnidadHora", PerInca.Unidad_H);
            int i = cmd.ExecuteNonQuery();

            con.Close();

            return i;
        }
        }

    }
}