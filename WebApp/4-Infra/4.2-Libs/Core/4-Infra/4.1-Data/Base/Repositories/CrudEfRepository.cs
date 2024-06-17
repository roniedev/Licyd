using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core
{
    public abstract class CrudEfRepository<T> : ICrudEfRepository<T> where T : class, IBaseAuditEntity
    {
        protected readonly DbSet<T> _dbSet;
        protected readonly AppSettings _appSettings;
        protected readonly BaseDbContext _context;

        protected CrudEfRepository(BaseDbContext context, AppSettings appSettings)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _appSettings = appSettings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public virtual void Activate(long id)
        {
            var entity = _dbSet.Find(id);

            (entity as IBaseStatusEntity).ReasonInactivation = "";
            (entity as IBaseStatusEntity).StatusId = StatusEnum.Active;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reasonInactivation"></param>
        public virtual void Inactivate(long id, string reasonInactivation)
        {
            var entity = _dbSet.Find(id);

            (entity as IBaseStatusEntity).StatusId = StatusEnum.Inactive;
            (entity as IBaseStatusEntity).ReasonInactivation = reasonInactivation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual T Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(long id)
        {
            var entity = GetById(id, false);
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual T Insert(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asNoTracking"></param>
        /// <returns></returns>
        public virtual T GetById(long id, bool asNoTracking = true)
        {
            var entity = asNoTracking
                ? _dbSet.AsNoTracking().Where(x => x.Id == id)
                : _dbSet.Where(x => x.Id == id);

            return SetIncludeOrFilter(entity).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll(List<Filter> filters)
        {
            var query = _dbSet.AsQueryable();
            var rows = QueryHelper.SetFiltersAndOrderBy(query, filters);
            return SetIncludeOrFilter(rows);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public virtual (IQueryable<T> rows, long totalRows) GetAllPaginated(GetAllInput viewModel)
        {
            var query = _dbSet.AsQueryable();

            var rows = QueryHelper.SetFiltersAndOrderBy(query, viewModel.Filters,
                viewModel.OrderBy, viewModel.IsAscending);

            rows = SetIncludeOrFilter(rows);
            return (rows, rows.Count());
        }

        /// <summary>
        /// Adiciona relacionamento em entidade, e filtros globais.
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        protected virtual IQueryable<T> SetIncludeOrFilter(IQueryable<T> queryable)
        {
            return queryable;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Commit()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Listar todos de acordo com a expressão linq
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>IQueryable<T></returns>
        public virtual IQueryable<T> GetAllWhere(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).AsNoTracking();
        }

        /// <summary>
        /// Listar um objeto de acordo com a expressão linq
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual T GetWhere(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).AsNoTracking().FirstOrDefault();
        }

        /// <summary>
        /// Campo para sobrescrever o comportamento de filtro, como cada entidade pode ter filtros diferente.
        /// Por padrão vai trazer todos os dados, sem filtragem
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll(string filter)
        {
            return _dbSet.AsNoTracking();
        }
    }
}
