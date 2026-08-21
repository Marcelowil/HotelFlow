namespace HotelFlow.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        void Salvar(T obj);
        T BuscarPorId(Guid id);

        IReadOnlyList<T> BuscarTodos();

    }
}
