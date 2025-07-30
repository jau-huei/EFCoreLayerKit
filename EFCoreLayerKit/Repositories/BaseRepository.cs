using EFCoreLayerKit.Data;
using EFCoreLayerKit.Entities;
using EFCoreLayerKit.Results;
using Microsoft.EntityFrameworkCore;

namespace EFCoreLayerKit.Repositories
{
    /// <summary>
    /// 通用实体仓储抽象基类，提供基本的增删改查操作。
    /// </summary>
    /// <typeparam name="TEntity">实体类型，必须继承自 BaseEntity</typeparam>
    public abstract class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly BaseDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(BaseDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// 根据主键 Id 异步获取实体。
        /// </summary>
        /// <param name="id">实体主键 Id。</param>
        /// <returns>包含操作结果和实体数据的 FResult 对象。</returns>
        public virtual async Task<FResult<TEntity>> GetByIdAsync(long id)
        {
            try
            {
                var entity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
                if (entity != null)
                {
                    return FResult<TEntity>.Ok(entity, "Entity found successfully.");
                }
                else
                {
                    return FResult<TEntity>.Fail("Entity of type {1} with Id {0} was not found.", ErrorCode.NotFound, null, id, typeof(TEntity).Name);
                }
            }
            catch (Exception ex)
            {
                return FResult<TEntity>.Fail("An exception occurred while querying the entity: {0}", ErrorCode.Exception, ex, ex.Message);
            }
        }

    }
}
