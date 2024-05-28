namespace BaseLib
{
    public interface ICrudRepository<T> where T : IBaseAuditEntity
    {
        /// <summary>
        /// Ativa um registro
        /// </summary>
        /// <param name="id"></param>
        void Activate(long id);

        /// <summary>
        /// Exclui um registro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void Delete(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        IQueryable<T> GetAll(List<Filter> filters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IQueryable<T> GetAll(string filter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        (IQueryable<T> rows, long totalRows) GetAllPaginated(GetAllInput viewModel);

        /// <summary>
        /// Inativa um registro com o motivo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reasonInactivation"></param>
        void Inactivate(long id, string reasonInactivation);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        T Insert(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Update(T entity);
    }
}
