using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using lab05.Data;
using lab05.Models;

namespace lab05.Repositories
{
    public class ReporteRepository
    {
        public List<ReporteDetalle> GenerarReporteFechas(DateTime inicio, DateTime fin)
        {
            var lista = new List<ReporteDetalle>();
            using (SqlConnection con = new SqlConnection(Conexion.Cadena))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ReportePedidosPorFecha", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FechaInicio", inicio.Date);
                    cmd.Parameters.AddWithValue("@FechaFin", fin.Date);

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteDetalle
                            {
                                PedidoID = Convert.ToInt32(dr["PedidoID"]),
                                FechaPedido = Convert.ToDateTime(dr["FechaPedido"]),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                Cantidad = Convert.ToInt16(dr["Cantidad"]),
                                PrecioUnidad = Convert.ToDecimal(dr["PrecioUnidad"]),
                                Total = Convert.ToDecimal(dr["Total"])
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}