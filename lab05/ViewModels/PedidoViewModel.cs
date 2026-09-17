namespace lab05.ViewModels
{
    public class PedidoViewModel : BaseViewModel
    {
        private string _mensaje;
        public string Mensaje
        {
            get => _mensaje;
            set { _mensaje = value; OnPropertyChanged(); }
        }

        public PedidoViewModel()
        {
            Mensaje = "Módulo de Pedidos (En construcción...)";
        }
    }
}