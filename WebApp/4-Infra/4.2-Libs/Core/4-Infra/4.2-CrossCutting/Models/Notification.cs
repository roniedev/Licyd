namespace Core;

/// <summary>
/// 
/// </summary>
public class Notification
{
    /// <summary>
    /// 
    /// </summary>
    public Notification()
    {
            
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="success"></param>
    public Notification(string message, bool success)
    {
        Message = message;
        Success = success;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <param name="success"></param>
    public Notification(string code, string message, bool success)
    {
        Code = code;
        Message = message;
        Success = success;
    }

    /// <summary>
    /// Código da notificação
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Descrição da notificação
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Indica se é uma noticação de erro ou sucesso
    /// </summary>
    public bool Success { get; set; } = false;
}
