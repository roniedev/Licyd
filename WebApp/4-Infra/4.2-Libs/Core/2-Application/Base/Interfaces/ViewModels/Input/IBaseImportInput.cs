using System.Text.Json.Serialization;

namespace Core
{
    public interface IBaseImportInput : IBaseInput
    {
        [JsonIgnore]
        [ExcelColumn(Name = "Sucesso", UseOnExport = true, UseOnImport = false, Order = 9998)]
        public bool IsSuccess { get; set; }

        [JsonIgnore]
        [ExcelColumn(Name = "Mensagem", UseOnExport = true, UseOnImport = false, Order = 9999)]
        public string Message { get; set; }
    }
}
