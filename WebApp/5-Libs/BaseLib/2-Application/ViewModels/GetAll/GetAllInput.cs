namespace BaseLib;

public class GetAllInput
{
    /// <summary>
    /// 
    /// </summary>
    public int Skip { get; set; } = 0;

    /// <summary>
    /// 
    /// </summary>
    public int Take { get; set; } = 8;

    /// <summary>
    /// 
    /// </summary>
    public string OrderBy { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public bool IsAscending { get; set; } = true;

    /// <summary>
    /// 
    /// </summary>
    public List<Filter> Filters { get; set; } = [];
}