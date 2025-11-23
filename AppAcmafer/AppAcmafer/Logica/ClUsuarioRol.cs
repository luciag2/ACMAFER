using AppAcmafer.Datos;
using System;
using System.Data;

namespace AppAcmafer.Logica
{
    public class Cl_UsuarioRol
    {
        private CD_UsuarioRol usuarioRolDAO = new CD_UsuarioRol();

        public DataTable ObtenerRolesDelUsuario(int idUsuario)
        {
            return usuarioRolDAO.ObtenerRolesUsuario(idUsuario);
        }

        public bool AsignarRolAUsuario(int idUsuario, int idRol)
        {
            return usuarioRolDAO.AsignarRol(idUsuario, idRol);
        }
    }
}