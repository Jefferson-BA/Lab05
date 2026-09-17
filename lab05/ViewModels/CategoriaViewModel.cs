using System.Collections.ObjectModel;
using System.Windows.Input;
using lab05.Commands;
using lab05.Models;
using lab05.Repositories;

namespace lab05.ViewModels
{
    public class CategoriaViewModel : BaseViewModel
    {
        private readonly CategoriaRepository _repo;

        public ObservableCollection<Categoria> Categorias { get; set; }

        public int CategoriaID { get; set; }
        public string NombreCategoria { get; set; }
        public string Descripcion { get; set; }

        public ICommand InsertarCommand { get; }
        public ICommand ActualizarCommand { get; }
        public ICommand EliminarCommand { get; }

        public CategoriaViewModel()
        {
            _repo = new CategoriaRepository();
            InsertarCommand = new RelayCommand(_ => Insertar());
            ActualizarCommand = new RelayCommand(_ => Actualizar());
            EliminarCommand = new RelayCommand(_ => Eliminar(), _ => CategoriaID > 0);
            Cargar();
        }

        private void Cargar()
        {
            Categorias = new ObservableCollection<Categoria>(_repo.Listar());
            OnPropertyChanged(nameof(Categorias));
        }

        private void Insertar() { _repo.Insertar(new Categoria { NombreCategoria = NombreCategoria, Descripcion = Descripcion }); Cargar(); }
        private void Actualizar() { _repo.Actualizar(new Categoria { CategoriaID = CategoriaID, NombreCategoria = NombreCategoria, Descripcion = Descripcion }); Cargar(); }
        private void Eliminar() { _repo.Eliminar(CategoriaID); Cargar(); }
    }
}