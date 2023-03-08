namespace HierarchyApp.Data.Implementation
{
    public interface IRepositoryBase<T>
    {
        public Task<T> GetById(int? id);
        public Task<T> Update( int id, T model);
        public Task<T> Delete(int id);
        public Task<T> Creat( T model);
    }
}
