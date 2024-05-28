using FluentValidation.Results;
using System.Net;

namespace BaseLib;

/// <summary>
/// 
/// </summary>
public class ResponseObject
{
    /// <summary>
    /// Código status HTTP da retorno da requisição
    /// </summary>
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

    /// <summary>
    /// Dados que serão retornados na requisição
    /// </summary>
    public object Data { get; set; } = null;

    /// <summary>
    /// Notificações adicionadas durante o requisição
    /// </summary>
    private readonly List<Notification> _notifications = [];

    /// <summary>
    /// Verifica se possui notificações
    /// </summary>
    public bool HasNotifications => _notifications.Count != 0;

    /// <summary>
    /// Recebe as notificações
    /// </summary>
    public IReadOnlyCollection<Notification> Notifications => _notifications;

    /// <summary>
    /// Adiciona uma notificação
    /// </summary>
    /// <param name="notification"></param>
    public void AddNotification(Notification notification)
    {
        _notifications.Add(notification);
    }

    /// <summary>
    /// Adiciona uma notificação
    /// </summary>
    /// <param name="descricao"></param>
    /// <param name="isSuccess"></param>
    public void AddNotification(string descricao, bool isSuccess)
    {
        _notifications.Add(new Notification
        {
            Message = descricao,
            Success = isSuccess
        });
    }

    /// <summary>
    /// Adiciona uma notificação
    /// </summary>
    /// <param name="message"></param>
    public void AddNotification(string message)
    {
        _notifications.Add(new Notification
        {
            Message = message
        });
    }

    /// <summary>
    /// Adiciona uma notificação
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    /// <param name="isSuccess"></param>
    public void AddNotification(string code, string message, bool isSuccess) 
    {
        _notifications.Add(new Notification
        {
            Code = code,
            Message = message,
            Success = isSuccess
        });
    }

    /// <summary>
    /// Adiciona uma notificação
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    public void AddNotification(string code, string message) 
    {
        _notifications.Add(new Notification
        {
            Code = code,
            Message = message,
        });
    }

    /// <summary>
    /// Adiciona uma notificação
    /// </summary>
    /// <param name="notificacao"></param>
    public void AddNotifications(IList<Notification> notificacao)
    {
        _notifications.AddRange(notificacao);
    }

    /// <summary>
    /// Adiciona uma notificação
    /// </summary>
    /// <param name="validationResult"></param>
    public void AddNotifications(ValidationResult validationResult)
    {
        if (validationResult == null)
        {
            AddNotification(
                "InternalServerError", 
                "Ocorreu um erro desconhecido, favor entrar em contato com o suporte.", 
                false);

            return;
        }

        foreach (var error in validationResult.Errors)
        {
            AddNotification(error.ErrorCode, error.ErrorMessage, false);
        }
    }
}