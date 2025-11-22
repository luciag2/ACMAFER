using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace AppAcmafer.Logica
{
    public class Cl_Usuario
    {
        // Método para VALIDAR email
        public bool ValidarEmail(string email)
        {
            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, patron);
        }

        // Método para VALIDAR documento
        public bool ValidarDocumento(string documento)
        {
            return !string.IsNullOrEmpty(documento) && documento.Length >= 6 && documento.Length <= 15;
        }

        // Método para VALIDAR celular
        public bool ValidarCelular(string celular)
        {
            return !string.IsNullOrEmpty(celular) && celular.Length == 10;
        }

        // Método para VALIDAR datos completos del usuario
        public bool ValidarDatosUsuario(Usuario usuario, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(usuario.Documento))
            {
                mensaje = "El documento es obligatorio";
                return false;
            }

            if (!ValidarDocumento(usuario.Documento))
            {
                mensaje = "El documento debe tener entre 6 y 15 caracteres";
                return false;
            }

            if (string.IsNullOrEmpty(usuario.Nombre))
            {
                mensaje = "El nombre es obligatorio";
                return false;
            }

            if (string.IsNullOrEmpty(usuario.Apellido))
            {
                mensaje = "El apellido es obligatorio";
                return false;
            }

            if (string.IsNullOrEmpty(usuario.Email))
            {
                mensaje = "El email es obligatorio";
                return false;
            }

            if (!ValidarEmail(usuario.Email))
            {
                mensaje = "El formato del email no es válido";
                return false;
            }

            if (string.IsNullOrEmpty(usuario.Celular))
            {
                mensaje = "El celular es obligatorio";
                return false;
            }

            if (!ValidarCelular(usuario.Celular))
            {
                mensaje = "El celular debe tener 10 dígitos";
                return false;
            }

            if (usuario.IdRol == 0)
            {
                mensaje = "Debe seleccionar un rol";
                return false;
            }

            return true;
        }

        // Método para ENCRIPTAR contraseña
        public string EncriptarClave(string clave)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(clave));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        // Método para VALIDAR login
        public bool ValidarLogin(string email, string clave, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(email))
            {
                mensaje = "Ingrese el email";
                return false;
            }

            if (string.IsNullOrEmpty(clave))
            {
                mensaje = "Ingrese la contraseña";
                return false;
            }

            return true;
        }
    }
}
