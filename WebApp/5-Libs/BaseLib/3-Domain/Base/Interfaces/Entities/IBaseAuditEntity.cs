namespace BaseLib
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBaseAuditEntity : IBaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DateTime CreatedAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string UpdatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DateTime UpdatedAt { get; set; }
    }
}
