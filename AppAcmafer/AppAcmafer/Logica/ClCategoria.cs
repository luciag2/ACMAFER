using AppAcmafer.Datos;
using AppAcmafer.Modelo;
using System;
using System.Collections.Generic;
using System.Data;

namespace AppAcmafer.Logica
{
    public class Cl_Categoria
    {
        private CategoriaDAO categoriaDAO = new CategoriaDAO();

        // Obtener todas las categorías como DataTable (para DropDownList)
        public DataTable ObtenerCategorias()
        {
            try
            {
                List<Categoria> categorias = categoriaDAO.ObtenerTodasLasCategorias();

                // Convertir List a DataTable
                DataTable dt = new DataTable();
                dt.Columns.Add("idCategoria", typeof(int));
                dt.Columns.Add("nombre", typeof(string));
                dt.Columns.Add("descripcion", typeof(string));

                foreach (Categoria cat in categorias)
                {
                    dt.Rows.Add(cat.IdCategoria, cat.Nombre, cat.Descripcion);
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en lógica al obtener categorías: " + ex.Message);
            }
        }

        // Obtener categoría por ID
        public Categoria ObtenerCategoriaPorId(int idCategoria)
        {
            try
            {
                return categoriaDAO.ObtenerCategoriaPorId(idCategoria);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en lógica al obtener categoría: " + ex.Message);
            }
        }

        // Insertar categoría
        public bool InsertarCategoria(string nombre, string descripcion)
        {
            try
            {
                Categoria categoria = new Categoria
                {
                    Nombre = nombre,
                    Descripcion = descripcion
                };

                return categoriaDAO.InsertarCategoria(categoria);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en lógica al insertar categoría: " + ex.Message);
            }
        }

        // Actualizar categoría
        public bool ActualizarCategoria(int idCategoria, string nombre, string descripcion)
        {
            try
            {
                Categoria categoria = new Categoria
                {
                    IdCategoria = idCategoria,
                    Nombre = nombre,
                    Descripcion = descripcion
                };

                return categoriaDAO.ActualizarCategoria(categoria);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en lógica al actualizar categoría: " + ex.Message);
            }
        }

        // Eliminar categoría
        public bool EliminarCategoria(int idCategoria)
        {
            try
            {
                return categoriaDAO.EliminarCategoria(idCategoria);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en lógica al eliminar categoría: " + ex.Message);
            }
        }

        // Validar nombre de categoría
        public bool ValidarNombre(string nombre)
        {
            return !string.IsNullOrEmpty(nombre) && nombre.Length >= 3 && nombre.Length <= 50;
        }

        // Validar datos completos
        public bool ValidarCategoria(string nombre, string descripcion, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(nombre))
            {
                mensaje = "El nombre de la categoría es obligatorio";
                return false;
            }

            if (!ValidarNombre(nombre))
            {
                mensaje = "El nombre debe tener entre 3 y 50 caracteres";
                return false;
            }

            if (string.IsNullOrEmpty(descripcion))
            {
                mensaje = "La descripción es obligatoria";
                return false;
            }

            return true;
        }
    }
}