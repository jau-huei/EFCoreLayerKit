using EFCoreLayerKit.Data;
using EFCoreLayerKit.Entities;
using EFCoreLayerKit.Results;
using Microsoft.EntityFrameworkCore;

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

    }
}
