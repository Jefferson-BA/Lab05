
using System.Data;
using System.Data.SqlClient;
using lab05.Data;
using lab05.Models;

namespace lab05.Repositories
{
    public class ProveedorRepository : IRepository<Proveedor>
    {
        public List<Proveedor> Listar()
        {
            return Buscar("", ""); // Reutilizamos la búsqueda vacía para listar todos
        }

        public List<Proveedor> Buscar(string contacto, string ciudad)
        {
            var lista = new List<Proveedor>();
            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                using (SqlCommand cmd = new SqlCommand("sp_BuscarProveedores", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreContacto", string.IsNullOrEmpty(contacto) ? (object)DBNull.Value : contacto);
                    cmd.Parameters.AddWithValue("@Ciudad", string.IsNullOrEmpty(ciudad) ? (object)DBNull.Value : ciudad);

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Proveedor
                            {
                                ProveedorID = Convert.ToInt32(dr["ProveedorID"]),
                                CompaniaNombre = dr["CompaniaNombre"].ToString(),
                                NombreContacto = dr["NombreContacto"].ToString(),
                                Ciudad = dr["Ciudad"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public void Insertar(Proveedor p)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertarProveedor", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CompaniaNombre", p.CompaniaNombre);
                    cmd.Parameters.AddWithValue("@NombreContacto", p.NombreContacto);
                    cmd.Parameters.AddWithValue("@Ciudad", p.Ciudad);
                    con.Open();
                    cmd.ExecuteNonQuery(); // ExecuteNonQuery implementado
                }
            }
        }

        public void Actualizar(Proveedor p)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ActualizarProveedor", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProveedorID", p.ProveedorID);
                    cmd.Parameters.AddWithValue("@CompaniaNombre", p.CompaniaNombre);
                    cmd.Parameters.AddWithValue("@NombreContacto", p.NombreContacto);
                    cmd.Parameters.AddWithValue("@Ciudad", p.Ciudad);
                    con.Open();
                    cmd.ExecuteNonQuery(); // ExecuteNonQuery implementado
                }
            }
        }

        public void Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                using (SqlCommand cmd = new SqlCommand("sp_EliminarProveedor", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProveedorID", id);
                    con.Open();
                    cmd.ExecuteNonQuery(); // ExecuteNonQuery para Baja Lógica
                }
            }
        }
    }
}