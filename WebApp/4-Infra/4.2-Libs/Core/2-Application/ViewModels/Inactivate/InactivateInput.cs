using System.ComponentModel.DataAnnotations;

namespace Core;

/// <summary>
/// 
/// </summary>
public class InactivateInput : BaseImportInput
{
    [Required(ErrorMessage = "O campo motivo da inativação é obrigatório.")]
    public string ReasonInactivation { get; set; }
}
