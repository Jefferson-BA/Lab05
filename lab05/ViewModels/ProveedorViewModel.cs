using System.Collections.ObjectModel;
using System.Windows.Input;
using lab05.Commands;
using lab05.Models;
using lab05.Repositories;

namespace lab05.ViewModels
{
    public class ProveedorViewModel : BaseViewModel
    {
        private readonly ProveedorRepository _repo;

        public ObservableCollection<Proveedor> Proveedores { get; set; }

        // Filtros de búsqueda
        public string FiltroContacto { get; set; }
        public string FiltroCiudad { get; set; }

        // Propiedades del formulario
        public int ProveedorID { get; set; }
        public string CompaniaNombre { get; set; }
        public string NombreContacto { get; set; }
        public string Ciudad { get; set; }

        public ICommand BuscarCommand { get; }
        public ICommand InsertarCommand { get; }
        public ICommand ActualizarCommand { get; } // <-- Comando Agregado
        public ICommand EliminarCommand { get; }

        public ProveedorViewModel()
        {
            _repo = new ProveedorRepository();
            BuscarCommand = new RelayCommand(_ => Buscar());
            InsertarCommand = new RelayCommand(_ => Insertar());
            ActualizarCommand = new RelayCommand(_ => Actualizar()); // <-- Inicialización Agregada
            EliminarCommand = new RelayCommand(_ => Eliminar(), _ => ProveedorID > 0);
            Buscar();
        }

        private void Buscar()
        {
            Proveedores = new ObservableCollection<Proveedor>(_repo.Buscar(FiltroContacto, FiltroCiudad));
            OnPropertyChanged(nameof(Proveedores));
        }

        private void Insertar()
        {
            _repo.Insertar(new Proveedor { CompaniaNombre = CompaniaNombre, NombreContacto = NombreContacto, Ciudad = Ciudad });
            Buscar();
        }

        // <-- Método Agregado
        private void Actualizar()
        {
            _repo.Actualizar(new Proveedor
            {
                ProveedorID = ProveedorID,
                CompaniaNombre = CompaniaNombre,
                NombreContacto = NombreContacto,
                Ciudad = Ciudad
            });
            Buscar();
        }

        private void Eliminar()
        {
            _repo.Eliminar(ProveedorID);
            Buscar();
        }
    }
}