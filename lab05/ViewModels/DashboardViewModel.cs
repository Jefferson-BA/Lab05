using System.Windows.Input;
using lab05.Commands;

namespace lab05.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set { _currentViewModel = value; OnPropertyChanged(); }
        }

        public ICommand MostrarProductosCommand { get; }
        public ICommand MostrarProveedoresCommand { get; }
        public ICommand MostrarCategoriasCommand { get; }
        public ICommand MostrarPedidosCommand { get; }
        public ICommand MostrarReportesCommand { get; }

        public DashboardViewModel()
        {
            MostrarProductosCommand = new RelayCommand(_ => CurrentViewModel = new ProductoViewModel());
            MostrarProveedoresCommand = new RelayCommand(_ => CurrentViewModel = new ProveedorViewModel());
            MostrarCategoriasCommand = new RelayCommand(_ => CurrentViewModel = new CategoriaViewModel());
            MostrarPedidosCommand = new RelayCommand(_ => CurrentViewModel = new PedidoViewModel());
            MostrarReportesCommand = new RelayCommand(_ => CurrentViewModel = new ReporteViewModel());

            // Vista por defecto al abrir el programa
            CurrentViewModel = new ProductoViewModel();
        }
    }
}