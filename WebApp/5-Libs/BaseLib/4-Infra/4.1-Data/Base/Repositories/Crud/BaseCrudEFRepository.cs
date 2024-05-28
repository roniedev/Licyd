//using Microsoft.EntityFrameworkCore;
//using System.Linq.Expressions;

//namespace BaseLib;

///// <summary>
///// 
///// </summary>
//public abstract class BaseCrudEFRepository<T> : BaseRepository, IBaseCrudEFRepository<T> where T : class, IBaseAuditEntity
//{
//    protected readonly DbSet<T> _dbSet;
//    protected readonly BaseDbContext _context;

//    protected BaseCrudEFRepository(AppSettings appSettings, 
//        ResponseObject responseObject, 
//        BaseDbContext context) : base(appSettings, responseObject)
//    {
//        _context = context;
//        _dbSet = _context.Set<T>();
//    }

//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="entity"></param>
//    public virtual T Update(T entity)
//    {
//        _context.Entry(entity).State = EntityState.Modified;
//        return entity;
//    }

//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="id"></param>
//    public virtual void Delete(long id)
//    {
//        var entity = GetById(id, false);
//        _dbSet.Remove(entity);
//    }


//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="entity"></param>
//    public virtual T Insert(T entity)
//    {
//        _dbSet.Add(entity);
//        return entity;
//    }

//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="id"></param>
//    /// <param name="asNoTracking"></param>
//    /// <returns></returns>
//    public virtual T GetById(long id, bool asNoTracking = true)
//    {
//        var entity = asNoTracking
//            ? _dbSet.AsNoTracking().Where(x => x.Id == id)
//            : _dbSet.Where(x => x.Id == id);

//        return SetIncludeOrFilter(entity).FirstOrDefault();
//    }

//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="filters"></param>
//    /// <returns></returns>
//    public virtual IQueryable<T> GetAll(List<Filter> filters)
//    {
//        var query = _dbSet.AsQueryable();
//        var rows = QueryHelper.SetFiltersAndOrderBy(query, filters);
//        return SetIncludeOrFilter(rows);
//    }

//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="viewModel"></param>
//    /// <returns></returns>
//    public virtual (IQueryable<T> rows, long totalRows) GetAllPaginated(GetAllViewModel viewModel)
//    {
//        var query = _dbSet.AsQueryable();

//        var rows = QueryHelper.SetFiltersAndOrderBy(query, viewModel.Filters,
//            viewModel.OrderBy, viewModel.IsAscending);

//        rows = SetIncludeOrFilter(rows);
//        return (rows, rows.Count());
//    }

//    /// <summary>
//    /// Adiciona relacionamento em entidade, e filtros globais.
//    /// </summary>
//    /// <param name="queryable"></param>
//    /// <returns></returns>
//    protected virtual IQueryable<T> SetIncludeOrFilter(IQueryable<T> queryable)
//    {
//        return queryable;
//    }

//    /// <summary>
//    /// 
//    /// </summary>
//    public void Commit()
//    {
//        _context.SaveChanges();
//    }

//    public virtual IQueryable<T> GetAllWhere(Expression<Func<T, bool>> expression)
//    {
//        return _dbSet.Where(expression).AsNoTracking();
//    }

//    public virtual T GetWhere(Expression<Func<T, bool>> expression)
//    {
//        return _dbSet.Where(expression).AsNoTracking().FirstOrDefault();
//    }

//    /// <summary>
//    /// Campo para sobrescrever o comportamento de filtro, como cada entidade pode ter filtros diferente.
//    /// Por padrão vai trazer todos os dados, sem filtragem
//    /// </summary>
//    /// <param name="filter"></param>
//    /// <returns></returns>
//    public virtual IQueryable<T> GetAll(string filter)
//    {
//        return _dbSet.AsNoTracking();
//    }
//}
