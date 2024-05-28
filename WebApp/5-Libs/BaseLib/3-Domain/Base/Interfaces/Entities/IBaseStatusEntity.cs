namespace BaseLib
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBaseStatusEntity : IBaseAuditEntity
    {
        /// <summary>
        /// 
        /// </summary>
        StatusEnum StatusId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ReasonInactivation { get; set; }
    }
}
