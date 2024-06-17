using System.Text.Json.Serialization;

namespace Core
{
    public class BaseImportInput : IBaseImportInput
    {
        [ExcelColumn(Name = "Identificador (código)", UseOnExport = true, UseOnImport = true, Order = -9999)]
        public long? Id { get; set; }

        [JsonIgnore]
        [ExcelColumn(Name = "Sucesso", UseOnExport = true, UseOnImport = false, Order = 9998)]
        public bool IsSuccess { get; set; }

        [JsonIgnore]
        [ExcelColumn(Name = "Mensagem", UseOnExport = true, UseOnImport = false, Order = 9999)]
        public string Message { get; set; }
    }
}
