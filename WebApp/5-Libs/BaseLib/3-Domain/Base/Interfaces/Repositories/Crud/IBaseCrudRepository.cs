//namespace BaseLib;

///// <summary>
///// 
///// </summary>
///// <typeparam name="T"></typeparam>
//public interface IBaseCrudRepository<T> : IBaseRepository where T : IBaseAuditEntity
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="id"></param>
//    /// <returns></returns>
//    void Delete(long id);

//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="filtros"></param>
//    /// <returns></returns>
//    IQueryable<T> GetAll(List<Filter> filters);


//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="filter"></param>
//    /// <returns></returns>
//    IQueryable<T> GetAll(string filter);

//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="viewModel"></param>
//    /// <returns></returns>
//    (IQueryable<T> rows, long totalRows) GetAllPaginated(GetAllViewModel viewModel);

//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="entity"></param>
//    T Insert(T entity);

//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="entity"></param>
//    /// <returns></returns>
//    T Update(T entity);
//}
