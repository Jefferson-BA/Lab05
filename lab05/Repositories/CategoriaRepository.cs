using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using lab05.Data;
using lab05.Models;

namespace lab05.Repositories
{
    public class CategoriaRepository : IRepository<Categoria>
    {
        public List<Categoria> Listar()
        {
            var lista = new List<Categoria>();
            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ListarCategorias", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Categoria
                            {
                                CategoriaID = Convert.ToInt32(dr["CategoriaID"]),
                                NombreCategoria = dr["NombreCategoria"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public void Insertar(Categoria c)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertarCategoria", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreCategoria", c.NombreCategoria);
                    cmd.Parameters.AddWithValue("@Descripcion", c.Descripcion);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Categoria c)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ActualizarCategoria", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoriaID", c.CategoriaID);
                    cmd.Parameters.AddWithValue("@NombreCategoria", c.NombreCategoria);
                    cmd.Parameters.AddWithValue("@Descripcion", c.Descripcion);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                using (SqlCommand cmd = new SqlCommand("sp_EliminarCategoria", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoriaID", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}