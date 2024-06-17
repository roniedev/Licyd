using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Core;

/// <summary>
/// Base controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
[EnableCors("CorsPolicy")]
[TypeFilter(typeof(ExceptionFilter))]
[TypeFilter(typeof(NotificationFilter))]
public abstract class BaseController(AppSettings appSettings, ResponseObject responseObject) : ControllerBase
{
    /// <summary>
    /// Classe com os dados do arquivo de configuração
    /// </summary>
    protected readonly AppSettings _appSettings = appSettings;

    /// <summary>
    /// Objeto que irá receber as notificações durante a execução
    /// </summary>
    protected readonly ResponseObject _responseObject = responseObject;
}