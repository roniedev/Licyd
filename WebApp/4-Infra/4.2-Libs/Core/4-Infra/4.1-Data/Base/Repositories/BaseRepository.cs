namespace Core;

/// <summary>
/// Base repository
/// </summary>
/// <param name="appSettings"></param>
/// <param name="responseObject"></param>
public abstract class BaseRepository(AppSettings appSettings, ResponseObject responseObject)
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
