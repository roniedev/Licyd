namespace Core;

public class BaseOutput : IBaseOutput
{
    [ExcelColumn(Name = "Identificador (código)", Order = -9999)]
    public long Id { get; set; }

    [ExcelColumn(Name = "Criado Por", Order = 9994)]
    public string CreatedBy { get; set; }

    [ExcelColumn(Name = "Criado Em", Order = 9995)]
    public DateTime CreatedAt { get; set; }

    [ExcelColumn(Name = "Atualizado Por", Order = 9996)]
    public string UpdatedBy { get; set; }

    [ExcelColumn(Name = "Atualizado Em", Order = 9997)]
    public DateTime UpdatedAt { get; set; }
}
