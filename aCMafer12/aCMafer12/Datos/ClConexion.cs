using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;

namespace AppAcmafer.Datos
{
    public class ClConexion
    {
        private SqlConnection oConex;

        public ClConexion()
        {
            oConex = new SqlConnection("Data Source=DESKTOP-48HI8BK;Initial Catalog=ProyectoACMAFER;Integrated Security=True;");
        }


        public SqlConnection MtAbrirConexion()
        {
            oConex.Open();
            return oConex;
        }


        public void MtCerrarConexion()
        {
            if (oConex.State == ConnectionState.Open)
            {
                oConex.Close();
            }
        }

        public SqlConnection ObtenerConexion()
        {
            return oConex;
        }
    }
}






