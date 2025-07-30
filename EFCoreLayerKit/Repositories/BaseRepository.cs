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
    /// ͨ��ʵ��ִ�������࣬�ṩ��������ɾ�Ĳ������
    /// </summary>
    /// <typeparam name="TEntity">ʵ�����ͣ�����̳��� BaseEntity</typeparam>
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
        /// �������� Id �첽��ȡʵ�塣
        /// </summary>
        /// <param name="id">ʵ������ Id��</param>
        /// <returns>�������������ʵ�����ݵ� FResult ����</returns>
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
        /// ��ȡ����ʵ���б�
        /// </summary>
        /// <param name="options">��ѡ��ѯ����</param>
        /// <returns>�������������ʵ�������б�� FResult ����</returns>
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
        /// �����������ʽ��ѯʵ���б�
        /// </summary>
        /// <param name="predicate">��ѯ�������ʽ��</param>
        /// <param name="options">��ѡ��ѯ����</param>
        /// <returns>�������������ʵ�������б�� FResult ����</returns>
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
        /// �ж��Ƿ��������������ʵ�塣
        /// </summary>
        /// <param name="predicate">��ѯ�������ʽ��</param>
        /// <returns>������������Ͳ���ֵ�� FResult ����</returns>
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
        /// ��ȡ����������ʵ��������
        /// </summary>
        /// <param name="predicate">��ѯ�������ʽ��</param>
        /// <returns>������������������� FResult ����</returns>
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
        /// ��ҳ��ѯʵ���б�֧�����򣩡�
        /// </summary>
        /// <param name="predicate">��ѯ�������ʽ��</param>
        /// <param name="pageIndex">��ǰҳ�루��1��ʼ����</param>
        /// <param name="pageSize">ÿҳ������</param>
        /// <param name="options">��ѡ��ѯ����</param>
        /// <returns>������������ͷ�ҳ���ݵ� FPagedResult ����</returns>
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
        /// �첽����ʵ�塣
        /// <para>ע�⣺</para>
        /// <list type="bullet">
        /// <item>��ʵ��̳� UpdatableEntity��������ֹ���У�飨���� UpdatedAt �ֶΣ���</item>
        /// <item>�ֹ���У��ͨ�����Զ��� UpdatedAt ����Ϊ��ǰʱ�䡣</item>
        /// <item>��������ʵ����ֶΣ������Զ��������µ������ԣ��缯��/���õ��ӱ����ݣ���</item>
        /// <item>������µ������ԣ����ӱ�������ҵ������д������ʵ���״̬�� SaveChanges��</item>
        /// <item>�� BaseEntity ʱ��ֱ�Ӹ��� Id ȫ�����������ֶΡ�</item>
        /// <item>���³ɹ���������ʵ�壬���汾��ͻ�򷵻ش���</item>
        /// </list>
        /// </summary>
        /// <param name="entity">Ҫ���µ�ʵ�����</param>
        /// <returns>�����������������ʵ�����ݵ� FResult ����</returns>
        public virtual async Task<FResult<TEntity>> UpdateAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                    return FResult<TEntity>.Fail("Entity cannot be null.", ErrorCode.InvalidParameter);

                // �ж��Ƿ�Ϊ UpdatableEntity
                if (entity is UpdatableEntity updatable)
                {
                    // �������ݿ��е�ԭʼʵ��
                    var dbEntity = await _dbSet.FirstOrDefaultAsync(e => e.Id == entity.Id);
                    if (dbEntity == null)
                        return FResult<TEntity>.Fail("Entity to update was not found.", ErrorCode.NotFound, entity.Id);

                    // ��� UpdatedAt �Ƿ�һ��
                    var dbUpdatedAt = (dbEntity as UpdatableEntity)?.UpdatedAt;
                    if (dbUpdatedAt == null || dbUpdatedAt != updatable.UpdatedAt)
                        return FResult<TEntity>.Fail("Entity has been modified by others. Please refresh and try again.", ErrorCode.Conflict, entity.Id);

                    // ����ʱ��
                    updatable.UpdatedAt = DateTime.Now;

                    // ������������
                    _context.Entry(dbEntity).CurrentValues.SetValues(entity);

                    // ��֤ UpdatedAt ������
                    (dbEntity as UpdatableEntity)!.UpdatedAt = updatable.UpdatedAt;
                }
                else
                {
                    // �� BaseEntity��ֱ�Ӹ���
                    _dbSet.Attach(entity);
                    _context.Entry(entity).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();

                // ��������ʵ��
                return await GetByIdAsync(entity.Id);
            }
            catch (Exception ex)
            {
                return FResult<TEntity>.Fail("Exception occurred while updating entity: {0}", ErrorCode.Exception, ex, ex.Message);
            }
        }

        /// <summary>
        /// ����ʵ�塣
        /// <para>�� Id=0 ʱ�����ݿ��Զ���������������ʹ��ԭ Id���������ظ��򱨴�</para>
        /// </summary>
        /// <param name="entity">Ҫ������ʵ�����</param>
        /// <returns>�����������������ʵ�����ݵ� FResult ����</returns>
        public virtual async Task<FResult<TEntity>> AddAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                    return FResult<TEntity>.Fail("Entity cannot be null.", ErrorCode.InvalidParameter);

                if (entity.Id != 0)
                {
                    // ��������Ƿ��Ѵ���
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
                // ������ͻ�����ݿ��쳣
                return FResult<TEntity>.Fail("Database error occurred while adding entity: {0}", ErrorCode.DatabaseUpdateFailed, dbEx, dbEx.Message);
            }
            catch (Exception ex)
            {
                return FResult<TEntity>.Fail("Exception occurred while adding entity: {0}", ErrorCode.Exception, ex, ex.Message);
            }
        }

        /// <summary>
        /// ����ɾ��ʵ�壨�����ݿ����Ƴ�����
        /// </summary>
        /// <param name="id">Ҫɾ����ʵ������ Id��</param>
        /// <returns>������������� FResult ����</returns>
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
        /// ��ɾ��ʵ�壨�� IsDeleted ��Ϊ true������ʵ���� IsDeleted ����ʱ��Ч����
        /// </summary>
        /// <param name="id">Ҫ��ɾ����ʵ������ Id��</param>
        /// <returns>������������� FResult ����</returns>
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
