        Task CreateAsync(TEntity item);
        Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task RemoveAsync(TEntity item);
        Task UpdateAsync(TEntity item);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindByIdAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);