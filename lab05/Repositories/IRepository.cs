using System.Collections.Generic;

namespace lab05.Repositories
{
    public interface IRepository<T>
    {
        List<T> Listar();
        void Insertar(T entidad);
        void Actualizar(T entidad);
        void Eliminar(int id);
    }
}