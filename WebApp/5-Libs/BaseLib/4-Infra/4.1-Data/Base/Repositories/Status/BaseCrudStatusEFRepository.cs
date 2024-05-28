//namespace BaseLib;

//public abstract class BaseCrudStatusEFRepository<T>(AppSettings appSettings, 
//    ResponseObject responseObject, BaseDbContext context) : 
//    BaseCrudEFRepository<T>(appSettings, responseObject, context), 
//    IBaseCrudStatusRepository<T> where T : class, IBaseStatusEntity
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="id"></param>
//    public virtual void Activate(long id)
//    {
//        var entity = _dbSet.Find(id);

//        entity.ReasonInactivation = "";
//        entity.StatusId = StatusEnum.Active;
//    }

//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="id"></param>
//    /// <param name="reasonInactivation"></param>
//    public virtual void Inactivate(long id, string reasonInactivation)
//    {
//        var entidade = _dbSet.Find(id);

//        entidade.StatusId = StatusEnum.Inactive;
//        entidade.ReasonInactivation = reasonInactivation;
//    }
//}
