using EFCoreLayerKit.Core;
using EFCoreLayerKit.Data;
using EFCoreLayerKit.Entities;
using EFCoreLayerKit.QueryModels;
using EFCoreLayerKit.Results;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

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

        /// <summary>
        /// 获取所有实体列表。
        /// </summary>
        /// <param name="options">可选查询规则。</param>
        /// <returns>包含操作结果和实体数据列表的 FResult 对象。</returns>
        public virtual async Task<FResult<List<TEntity>>> GetAllAsync(QueryOptions<TEntity>? options = null)
        {
            try
            {
                var query = _dbSet.AsNoTracking().AsQueryable();
                query = query.ApplyQueryOption(options);
                var list = await query.ToListAsync();
                return FResult<List<TEntity>>.Ok(list, "All entities fetched successfully.");
            }
            catch (Exception ex)
            {
                return FResult<List<TEntity>>.Fail("An exception occurred while fetching all entities: {0}", ErrorCode.Exception, ex, ex.Message);
            }
        }

        /// <summary>
        /// 根据条件表达式查询实体列表。
        /// </summary>
        /// <param name="predicate">查询条件表达式。</param>
        /// <param name="options">可选查询规则。</param>
        /// <returns>包含操作结果和实体数据列表的 FResult 对象。</returns>
        public virtual async Task<FResult<List<TEntity>>> FindAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, QueryOptions<TEntity>? options = null)
        {
            try
            {
                var query = _dbSet.AsNoTracking().Where(predicate);
                query = query.ApplyQueryOption(options);
                var list = await query.ToListAsync();
                return FResult<List<TEntity>>.Ok(list, "Entities fetched by condition successfully.");
            }
            catch (Exception ex)
            {
                return FResult<List<TEntity>>.Fail("An exception occurred while querying entities: {0}", ErrorCode.Exception, ex, ex.Message);
            }
        }

        /// <summary>
        /// 判断是否存在满足条件的实体。
        /// </summary>
        /// <param name="predicate">查询条件表达式。</param>
        /// <returns>包含操作结果和布尔值的 FResult 对象。</returns>
        public virtual async Task<FResult<bool>> ExistsAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var exists = await _dbSet.AsNoTracking().AnyAsync(predicate);
                return FResult<bool>.Ok(exists, exists ? "Entity exists." : "Entity does not exist.");
            }
            catch (Exception ex)
            {
                return FResult<bool>.Fail("An exception occurred while checking existence: {0}", ErrorCode.Exception, ex, ex.Message);
            }
        }

        /// <summary>
        /// 获取满足条件的实体数量。
        /// </summary>
        /// <param name="predicate">查询条件表达式。</param>
        /// <returns>包含操作结果和数量的 FResult 对象。</returns>
        public virtual async Task<FResult<int>> CountAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var count = await _dbSet.AsNoTracking().CountAsync(predicate);
                return FResult<int>.Ok(count, "Count fetched successfully.");
            }
            catch (Exception ex)
            {
                return FResult<int>.Fail("An exception occurred while counting entities: {0}", ErrorCode.Exception, ex, ex.Message);
            }
        }

        /// <summary>
        /// 分页查询实体列表（支持排序）。
        /// </summary>
        /// <param name="predicate">查询条件表达式。</param>
        /// <param name="pageIndex">当前页码（从1开始）。</param>
        /// <param name="pageSize">每页数量。</param>
        /// <param name="options">可选查询规则。</param>
        /// <returns>包含操作结果和分页数据的 FPagedResult 对象。</returns>
        public virtual async Task<FPagedResult<TEntity>> GetPagedAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize, QueryOptions<TEntity>? options = null)
        {
            try
            {
                var query = _dbSet.AsNoTracking().Where(predicate);
                query = query.ApplyQueryOption(options);
                var total = await query.CountAsync();
                var data = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                return FPagedResult<TEntity>.Ok(data, total, pageIndex, pageSize, "Paged entities fetched successfully.");
            }
            catch (Exception ex)
            {
                return FPagedResult<TEntity>.Fail("An exception occurred while paged querying: {0}", ErrorCode.Exception, ex, ex.Message);
            }
        }

        /// <summary>
        /// 异步更新实体。
        /// <para>注意：</para>
        /// <list type="bullet">
        /// <item>若实体继承 UpdatableEntity，则进行乐观锁校验（基于 UpdatedAt 字段）。</item>
        /// <item>乐观锁校验通过后，自动将 UpdatedAt 设置为当前时间。</item>
        /// <item>仅更新主实体表字段，不会自动级联更新导航属性（如集合/引用的子表数据）。</item>
        /// <item>如需更新导航属性（如子表），请在业务层自行处理相关实体的状态和 SaveChanges。</item>
        /// <item>仅 BaseEntity 时，直接根据 Id 全量更新主表字段。</item>
        /// <item>更新成功返回最新实体，若版本冲突则返回错误。</item>
        /// </list>
        /// </summary>
        /// <param name="entity">要更新的实体对象。</param>
        /// <returns>包含操作结果和最新实体数据的 FResult 对象。</returns>
        public virtual async Task<FResult<TEntity>> UpdateAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                    return FResult<TEntity>.Fail("Entity cannot be null.", ErrorCode.InvalidParameter);

                // 判断是否为 UpdatableEntity
                if (entity is UpdatableEntity updatable)
                {
                    // 查找数据库中的原始实体
                    var dbEntity = await _dbSet.FirstOrDefaultAsync(e => e.Id == entity.Id);
                    if (dbEntity == null)
                        return FResult<TEntity>.Fail("Entity to update was not found.", ErrorCode.NotFound, entity.Id);

                    // 检查 UpdatedAt 是否一致
                    var dbUpdatedAt = (dbEntity as UpdatableEntity)?.UpdatedAt;
                    if (dbUpdatedAt == null || dbUpdatedAt != updatable.UpdatedAt)
                        return FResult<TEntity>.Fail("Entity has been modified by others. Please refresh and try again.", ErrorCode.Conflict, entity.Id);

                    // 更新时间
                    updatable.UpdatedAt = DateTime.Now;

                    // 更新所有属性
                    _context.Entry(dbEntity).CurrentValues.SetValues(entity);

                    // 保证 UpdatedAt 是最新
                    (dbEntity as UpdatableEntity)!.UpdatedAt = updatable.UpdatedAt;
                }
                else
                {
                    // 仅 BaseEntity，直接更新
                    _dbSet.Attach(entity);
                    _context.Entry(entity).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();

                // 返回最新实体
                return await GetByIdAsync(entity.Id);
            }
            catch (Exception ex)
            {
                return FResult<TEntity>.Fail("Exception occurred while updating entity: {0}", ErrorCode.Exception, ex, ex.Message);
            }
        }

        /// <summary>
        /// 新增实体。
        /// <para>当 Id=0 时，数据库自动递增主键；否则使用原 Id，若主键重复则报错。</para>
        /// </summary>
        /// <param name="entity">要新增的实体对象。</param>
        /// <returns>包含操作结果和新增实体数据的 FResult 对象。</returns>
        public virtual async Task<FResult<TEntity>> AddAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                    return FResult<TEntity>.Fail("Entity cannot be null.", ErrorCode.InvalidParameter);

                if (entity.Id != 0)
                {
                    // 检查主键是否已存在
                    var exists = await _dbSet.AsNoTracking().AnyAsync(e => e.Id == entity.Id);
                    if (exists)
                        return FResult<TEntity>.Fail("Entity with the same Id already exists.", ErrorCode.DuplicateData, entity.Id);
                }

                _dbSet.Add(entity);
                await _context.SaveChangesAsync();
                return FResult<TEntity>.Ok(entity, "Entity added successfully.");
            }
            catch (DbUpdateException dbEx)
            {
                // 主键冲突等数据库异常
                return FResult<TEntity>.Fail("Database error occurred while adding entity: {0}", ErrorCode.DatabaseUpdateFailed, dbEx, dbEx.Message);
            }
            catch (Exception ex)
            {
                return FResult<TEntity>.Fail("Exception occurred while adding entity: {0}", ErrorCode.Exception, ex, ex.Message);
            }
        }

        /// <summary>
        /// 物理删除实体（从数据库中移除）。
        /// </summary>
        /// <param name="id">要删除的实体主键 Id。</param>
        /// <returns>包含操作结果的 FResult 对象。</returns>
        public virtual async Task<FResult> DeleteAsync(long id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null)
                    return FResult.Fail("Entity to delete was not found.", ErrorCode.NotFound, id);

                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return FResult.Ok("Entity deleted successfully.");
            }
            catch (Exception ex)
            {
                return FResult.Fail("Exception occurred while deleting entity: {0}", ErrorCode.Exception, ex, ex.Message);
            }
        }

        /// <summary>
        /// 软删除实体（将 IsDeleted 设为 true，仅当实体有 IsDeleted 属性时有效）。
        /// </summary>
        /// <param name="id">要软删除的实体主键 Id。</param>
        /// <returns>包含操作结果的 FResult 对象。</returns>
        public virtual async Task<FResult> SoftDeleteAsync(long id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null)
                    return FResult.Fail("Entity to soft delete was not found.", ErrorCode.NotFound, id);

                var prop = typeof(TEntity).GetProperty("IsDeleted");
                if (prop == null || prop.PropertyType != typeof(bool))
                    return FResult.Fail("Entity does not support soft delete (missing IsDeleted property).", ErrorCode.OperationFailed, id);

                prop.SetValue(entity, true);
                await _context.SaveChangesAsync();
                return FResult.Ok("Entity soft deleted successfully.");
            }
            catch (Exception ex)
            {
                return FResult.Fail("Exception occurred while soft deleting entity: {0}", ErrorCode.Exception, ex, ex.Message);
            }
        }
    }
}
