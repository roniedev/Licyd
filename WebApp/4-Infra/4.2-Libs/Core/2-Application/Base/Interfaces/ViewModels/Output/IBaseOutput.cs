namespace Core;

/// <summary>
/// 
/// </summary>
public interface IBaseOutput
{
    /// <summary>
    /// 
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string CreatedBy { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string UpdatedBy { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}
