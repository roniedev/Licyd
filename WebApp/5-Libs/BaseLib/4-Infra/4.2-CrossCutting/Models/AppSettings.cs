namespace BaseLib;

/// <summary>
/// 
/// </summary>
public class AppSettings
{
    /// <summary>
    /// 
    /// </summary>
    public long ApplicationId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string ApplicationName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string ApplicationCode { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string IsDevEnv { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string UrlReportServer { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string UrlLxConnector { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string EnvReport { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool IsUpperCaseText { get; set; }

    /// <summary>
    /// Chave JWT
    /// </summary>
    public string JwtKey { get; set; }

    /// <summary>
    /// Validade do token em horas
    /// </summary>
    public int TokenJwtExpiration { get; set; }

    /// <summary>
    /// Habilita/Desabilita a auditoria das tabelas
    /// </summary>
    public bool IsAuditTrailEnabled { get; set; }

    /// <summary>
    /// Classe de conexões
    /// </summary>
    public Connection Connection { get; set; } = new Connection();
}
