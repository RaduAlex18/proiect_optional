using proiect_op_2_v3_final.Models.Base;

namespace proiect_op_2_v3_final.Repositories.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : BaseE
    {
        //get all data
        Task<List<TEntity>> GetAllAsync();

        //Create
        void Create(TEntity entity);
        Task CreateAsync(TEntity entity);
        void CreateRange(IEnumerable<TEntity> entities);
        Task CreateRangeAsync(IEnumerable<TEntity> entities);

        //Update
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);

        //Delete
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);

        //Find
        TEntity FindById(Guid id);
        Task<TEntity> FindByIdAsync(Guid id);

        //Save
        bool Save();
        Task<bool> SaveAsync();

    }
}
