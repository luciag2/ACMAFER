using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AppAcmafer.Datos
{
    public class ClUsuarioD
    {
        public listUsuarioM MtLogin(string user, string pass)
        {
            ClConexion oConexion = new ClConexion();
            listUsuarioM oDatosuser = null;

            string consulta = $@"SELECT * FROM usuario 
                        WHERE email = '{user}' AND clave = '{pass}'";

            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, oConexion .MtAbrirConexion());
            DataTable tblDatos = new DataTable();
            adaptador.Fill(tblDatos);

            if (tblDatos.Rows.Count == 1)
            {
                oDatosuser = new listUsuarioM();
                oDatosuser.IdUsuario = int.Parse(tblDatos.Rows[0]["idUsuario"].ToString());
                oDatosuser.Documento = tblDatos.Rows[0]["documento"].ToString();
                oDatosuser.Nombre = tblDatos.Rows[0]["nombre"].ToString();
                oDatosuser.Apellido = tblDatos.Rows[0]["apellido"].ToString();
                oDatosuser.Email = tblDatos.Rows[0]["email"].ToString();
                oDatosuser.Celular = tblDatos.Rows[0]["celular"].ToString();
                oDatosuser.Clave = tblDatos.Rows[0]["clave"].ToString();
                oDatosuser.Estado = tblDatos.Rows[0]["estado"].ToString();
                oDatosuser.idRol = int.Parse(tblDatos.Rows[0]["idRol"].ToString());
            }

            oConexion.MtCerrarConexion();
            return oDatosuser;
        }

       
    }
}

    



