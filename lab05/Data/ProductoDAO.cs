using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using lab05.Models;

namespace lab05.Data
{
    public class ProductoDAO
    {
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();
            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ListarProductos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto
                            {
                                ProductoID = Convert.ToInt32(dr["ProductoID"]),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                ProveedorID = dr["ProveedorID"] != DBNull.Value ? Convert.ToInt32(dr["ProveedorID"]) : (int?)null,
                                CategoriaID = dr["CategoriaID"] != DBNull.Value ? Convert.ToInt32(dr["CategoriaID"]) : (int?)null,
                                CantidadPorUnidad = dr["CantidadPorUnidad"].ToString(),
                                PrecioUnidad = Convert.ToDecimal(dr["PrecioUnidad"]),
                                UnidadesEnExistencia = Convert.ToInt16(dr["UnidadesEnExistencia"]),
                                UnidadesEnPedido = Convert.ToInt16(dr["UnidadesEnPedido"]),
                                NivelDeReorden = Convert.ToInt16(dr["NivelDeReorden"]),
                                Descontinuado = Convert.ToBoolean(dr["Descontinuado"]),
                                Activo = Convert.ToBoolean(dr["Activo"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public void Insertar(Producto p)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertarProducto", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreProducto", p.NombreProducto);
                    cmd.Parameters.AddWithValue("@ProveedorID", (object)p.ProveedorID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CategoriaID", (object)p.CategoriaID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CantidadPorUnidad", (object)p.CantidadPorUnidad ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PrecioUnidad", p.PrecioUnidad);
                    cmd.Parameters.AddWithValue("@UnidadesEnExistencia", p.UnidadesEnExistencia);
                    cmd.Parameters.AddWithValue("@UnidadesEnPedido", p.UnidadesEnPedido);
                    cmd.Parameters.AddWithValue("@NivelDeReorden", p.NivelDeReorden);
                    cmd.Parameters.AddWithValue("@Descontinuado", p.Descontinuado);

                    con.Open();
                    cmd.ExecuteNonQuery(); // Escritura
                }
            }
        }

        public void Actualizar(Producto p)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ActualizarProducto", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductoID", p.ProductoID);
                    cmd.Parameters.AddWithValue("@NombreProducto", p.NombreProducto);
                    cmd.Parameters.AddWithValue("@ProveedorID", (object)p.ProveedorID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CategoriaID", (object)p.CategoriaID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CantidadPorUnidad", (object)p.CantidadPorUnidad ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PrecioUnidad", p.PrecioUnidad);
                    cmd.Parameters.AddWithValue("@UnidadesEnExistencia", p.UnidadesEnExistencia);
                    cmd.Parameters.AddWithValue("@UnidadesEnPedido", p.UnidadesEnPedido);
                    cmd.Parameters.AddWithValue("@NivelDeReorden", p.NivelDeReorden);
                    cmd.Parameters.AddWithValue("@Descontinuado", p.Descontinuado);

                    con.Open();
                    cmd.ExecuteNonQuery(); // Escritura
                }
            }
        }

        public void Eliminar(int idProducto)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                using (SqlCommand cmd = new SqlCommand("sp_EliminarProducto", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductoID", idProducto);

                    con.Open();
                    cmd.ExecuteNonQuery(); // Eliminación Lógica
                }
            }
        }
    }
}