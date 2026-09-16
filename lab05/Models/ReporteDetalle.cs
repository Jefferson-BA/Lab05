namespace lab05.Models
{
    public class Proveedor
    {
        public int ProveedorID { get; set; }
        public string CompaniaNombre { get; set; }
        public string NombreContacto { get; set; }
        public string Ciudad { get; set; }
        public bool Activo { get; set; }
    }

    public class ReporteDetalle
    {
        public int PedidoID { get; set; }
        public System.DateTime FechaPedido { get; set; }
        public string NombreProducto { get; set; }
        public short Cantidad { get; set; }
        public decimal PrecioUnidad { get; set; }
        public decimal Total { get; set; }
    }
}