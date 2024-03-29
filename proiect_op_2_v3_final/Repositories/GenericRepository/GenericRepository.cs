﻿using Microsoft.EntityFrameworkCore;
using proiect_op_2_v3_final.Data;
using proiect_op_2_v3_final.Models.Base;

namespace proiect_op_2_v3_final.Repositories.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseE
    {
        protected readonly tableContext _tableContext;
        protected readonly DbSet<TEntity> _table;

        public GenericRepository(tableContext tableContext)
        {
            _tableContext = tableContext;
            _table = _tableContext.Set<TEntity>();
        }

        //Get all
        public List<TEntity> GetAll()
        {
            return _table.AsNoTracking().ToList();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _table.AsNoTracking().ToListAsync();
        }

        //Create
        public void Create(TEntity entity)
        {
            _table.Add(entity);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _table.AddAsync(entity);
        }

        public void CreateRange(IEnumerable<TEntity> entities)
        {
            _table.AddRange(entities);
        }

        public async Task CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _table.AddRangeAsync(entities);
        }

        //Update
        public void Update(TEntity entity)
        {
            _table.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _table.UpdateRange(entities);
        }

        //Delete
        public void Delete(TEntity entity)
        {
            _table.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _table.RemoveRange(entities);
        }

        // Find
        public TEntity FindById(Guid id)
        {
            return _table.Find(id);
        }

        public async Task<TEntity> FindByIdAsync(Guid id)
        {
            return await _table.FindAsync(id);
        }

        // Save
        public bool Save()
        {
            return _tableContext.SaveChanges() > 0;
        }
        public async Task<bool> SaveAsync()
        {
            return await _tableContext.SaveChangesAsync() > 0;
        }

    }
}
