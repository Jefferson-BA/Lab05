using System.Collections.ObjectModel;
using System.Windows.Input;
using lab05.Commands;
using lab05.Data;
using lab05.Models;

namespace lab05.ViewModels
{
    public class ProductoViewModel : BaseViewModel
    {
        private readonly ProductoDAO _dao;

        private ObservableCollection<Producto> _productos;
        public ObservableCollection<Producto> Productos
        {
            get => _productos;
            set { _productos = value; OnPropertyChanged(); }
        }

        private Producto _productoSeleccionado;
        public Producto ProductoSeleccionado
        {
            get => _productoSeleccionado;
            set
            {
                _productoSeleccionado = value;
                OnPropertyChanged();
                if (value != null) CargarDatosAlFormulario(value);
            }
        }

        // Propiedades del formulario enlazadas a la vista
        private int _productoID;
        public int ProductoID { get => _productoID; set { _productoID = value; OnPropertyChanged(); } }

        private string _nombreProducto;
        public string NombreProducto { get => _nombreProducto; set { _nombreProducto = value; OnPropertyChanged(); } }

        private int? _proveedorID;
        public int? ProveedorID { get => _proveedorID; set { _proveedorID = value; OnPropertyChanged(); } }

        private int? _categoriaID;
        public int? CategoriaID { get => _categoriaID; set { _categoriaID = value; OnPropertyChanged(); } }

        private string _cantidadPorUnidad;
        public string CantidadPorUnidad { get => _cantidadPorUnidad; set { _cantidadPorUnidad = value; OnPropertyChanged(); } }

        private decimal _precioUnidad;
        public decimal PrecioUnidad { get => _precioUnidad; set { _precioUnidad = value; OnPropertyChanged(); } }

        private short _unidadesEnExistencia;
        public short UnidadesEnExistencia { get => _unidadesEnExistencia; set { _unidadesEnExistencia = value; OnPropertyChanged(); } }

        private short _unidadesEnPedido;
        public short UnidadesEnPedido { get => _unidadesEnPedido; set { _unidadesEnPedido = value; OnPropertyChanged(); } }

        private short _nivelDeReorden;
        public short NivelDeReorden { get => _nivelDeReorden; set { _nivelDeReorden = value; OnPropertyChanged(); } }

        private bool _descontinuado;
        public bool Descontinuado { get => _descontinuado; set { _descontinuado = value; OnPropertyChanged(); } }

        // Comandos
        public ICommand CargarCommand { get; }
        public ICommand InsertarCommand { get; }
        public ICommand ActualizarCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand LimpiarCommand { get; }

        public ProductoViewModel()
        {
            _dao = new ProductoDAO();

            CargarCommand = new RelayCommand(_ => ListarProductos());
            InsertarCommand = new RelayCommand(_ => Insertar(), _ => !string.IsNullOrEmpty(NombreProducto));
            ActualizarCommand = new RelayCommand(_ => Actualizar(), _ => ProductoID > 0);
            EliminarCommand = new RelayCommand(_ => Eliminar(), _ => ProductoID > 0);
            LimpiarCommand = new RelayCommand(_ => Limpiar());

            ListarProductos();
        }

        private void ListarProductos()
        {
            Productos = new ObservableCollection<Producto>(_dao.Listar());
        }

        private void Insertar()
        {
            _dao.Insertar(CrearInstanciaDesdeFormulario());
            ListarProductos();
            Limpiar();
        }

        private void Actualizar()
        {
            _dao.Actualizar(CrearInstanciaDesdeFormulario());
            ListarProductos();
            Limpiar();
        }

        private void Eliminar()
        {
            _dao.Eliminar(ProductoID); // Ejecuta el UPDATE Activo = 0
            ListarProductos();
            Limpiar();
        }

        private Producto CrearInstanciaDesdeFormulario()
        {
            return new Producto
            {
                ProductoID = this.ProductoID,
                NombreProducto = this.NombreProducto,
                ProveedorID = this.ProveedorID,
                CategoriaID = this.CategoriaID,
                CantidadPorUnidad = this.CantidadPorUnidad,
                PrecioUnidad = this.PrecioUnidad,
                UnidadesEnExistencia = this.UnidadesEnExistencia,
                UnidadesEnPedido = this.UnidadesEnPedido,
                NivelDeReorden = this.NivelDeReorden,
                Descontinuado = this.Descontinuado
            };
        }

        private void CargarDatosAlFormulario(Producto p)
        {
            ProductoID = p.ProductoID;
            NombreProducto = p.NombreProducto;
            ProveedorID = p.ProveedorID;
            CategoriaID = p.CategoriaID;
            CantidadPorUnidad = p.CantidadPorUnidad;
            PrecioUnidad = p.PrecioUnidad;
            UnidadesEnExistencia = p.UnidadesEnExistencia;
            UnidadesEnPedido = p.UnidadesEnPedido;
            NivelDeReorden = p.NivelDeReorden;
            Descontinuado = p.Descontinuado;
        }

        private void Limpiar()
        {
            ProductoID = 0; NombreProducto = string.Empty; ProveedorID = null; CategoriaID = null;
            CantidadPorUnidad = string.Empty; PrecioUnidad = 0; UnidadesEnExistencia = 0;
            UnidadesEnPedido = 0; NivelDeReorden = 0; Descontinuado = false;
        }
    }
}