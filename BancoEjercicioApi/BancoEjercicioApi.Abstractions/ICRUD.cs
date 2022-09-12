namespace BancoEjercicioApi.Abstractions
{
    public interface ICRUD<T>
    {
        T Create(T entity);
        T Update(T entity);
        //T Save(T entity);
        IList<T> GetAll();
        T? GetById(int id);
        void DeleteById(int id);
    }
}