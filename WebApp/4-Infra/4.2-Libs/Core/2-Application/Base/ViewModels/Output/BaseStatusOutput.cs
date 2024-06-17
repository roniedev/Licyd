namespace Core;

public class BaseStatusOutput : BaseOutput, IBaseStatusOutput
{
    public StatusEnum StatusId { get; set; }

    [ExcelColumn(Name = "Descrição status", Order = 9992)]
    public string StatusName =>
        StatusId == StatusEnum.Build ? "Em construção" :
        StatusId == StatusEnum.BuildClone ? "Em construção (cópia)" :
        StatusId == StatusEnum.Active ? "Ativo" :
        StatusId == StatusEnum.Inactive ? "Inativo" : "";

    [ExcelColumn(Name = "Motivo da inativação", Order = 9993)]
    public string ReasonInactivation { get; set; }
}
