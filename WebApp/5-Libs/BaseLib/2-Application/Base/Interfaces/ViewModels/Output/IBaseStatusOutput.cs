namespace BaseLib;

public interface IBaseStatusOutput : IBaseOutput
{
    public StatusEnum StatusId { get; set; }

    public string ReasonInactivation { get; set; }
}