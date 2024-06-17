namespace Core;

public class BaseStatusEntity : BaseAuditEntity, IBaseStatusEntity
{
    /// <summary>
    /// Status do registro
    /// </summary>
   public StatusEnum StatusId { get; set; }

    /// <summary>
    /// Motivo da inativação
    /// </summary>
   public string ReasonInactivation { get; set; }

}
