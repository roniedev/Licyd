namespace BaseLib;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TOutput"></typeparam>
public class GetAllOutput<TOutput>
{
    /// <summary>
    /// 
    /// </summary>
    public IList<TOutput> Rows { get; set; } = new List<TOutput>();

    /// <summary>
    /// 
    /// </summary>
    public long TotalRows { get; set; } = 0;

    /// <summary>
    /// 
    /// </summary>
    public long TotalPages { get; set; } = 0;
}
