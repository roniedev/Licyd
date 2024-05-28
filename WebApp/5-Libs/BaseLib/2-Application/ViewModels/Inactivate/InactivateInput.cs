using System.ComponentModel.DataAnnotations;

namespace BaseLib;

/// <summary>
/// 
/// </summary>
public class InactivateInput : BaseImportInput
{
    [Required(ErrorMessage = "O campo motivo da inativação é obrigatório.")]
    public string ReasonInactivation { get; set; }
}
