using Microsoft.Extensions.Configuration;

namespace Core;

/// <summary>
/// 
/// </summary>
public class AppSettingsHelper
{
    /// <summary>
    /// 
    /// </summary>
    protected virtual AppSettings AppSettings { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected readonly IConfiguration _configuration;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    public AppSettingsHelper(IConfiguration configuration)
    {
        _configuration = configuration;
        AppSettings = new AppSettings();

        Descriptografar();
    }

    /// <summary>
    /// 
    /// </summary>
    protected virtual void Descriptografar()
    {
        var applicationId = _configuration.GetSection("AppSettings")["ApplicationId"];
        var applicationName = _configuration.GetSection("AppSettings")["ApplicationName"];
        var applicationCode = _configuration.GetSection("AppSettings")["ApplicationCode"];
        var version = _configuration.GetSection("AppSettings")["Version"];
        var isDevEnv = _configuration.GetSection("AppSettings")["IsDevEnv"];
        var urlReportServer = _configuration.GetSection("AppSettings")["UrlReportServer"];
        var urlLxConnector = _configuration.GetSection("AppSettings")["UrlLxConnector"];
        var envReport = _configuration.GetSection("AppSettings")["EnvReport"];
        var isUpperCaseText = _configuration.GetSection("AppSettings")["IsUpperCaseText"];
        var jwtKey = _configuration.GetSection("AppSettings")["JwtKey"];
        var isAuditTrailEnabled = _configuration.GetSection("AppSettings")["IsAuditTrailEnabled"];
        var tokenJwtExpiration = _configuration.GetSection("AppSettings")["TokenJwtExpiration"];

        var defaultConnection = _configuration.GetSection("AppSettings:Connection")["DefaultConnection"];
        var hangfire = _configuration.GetSection("AppSettings:Connection")["Hangfire"];
        var DB2 = _configuration.GetSection("AppSettings:Connection")["DB2"];
        var FPW = _configuration.GetSection("AppSettings:Connection")["FPW"];
        var InfoteutoWeb = _configuration.GetSection("AppSettings:Connection")["InfoteutoWeb"];
        var DTI0016 = _configuration.GetSection("AppSettings:Connection")["DTI0016"];
        var IsoWeb = _configuration.GetSection("AppSettings:Connection")["IsoWeb"];
        var Evolutio = _configuration.GetSection("AppSettings:Connection")["Evolutio"];
        var IsoWebIntegracao = _configuration.GetSection("AppSettings:Connection")["IsoWebIntegracao"];

        AppSettings.ApplicationId = long.Parse(applicationId);
        AppSettings.ApplicationName = applicationName;
        AppSettings.ApplicationCode = applicationCode;
        AppSettings.Version = version;
        AppSettings.IsDevEnv = isDevEnv;
        AppSettings.UrlReportServer = urlReportServer;
        AppSettings.EnvReport = envReport;
        AppSettings.JwtKey = CryptoHelper.Decrypt(jwtKey);
        AppSettings.IsAuditTrailEnabled = bool.Parse(isAuditTrailEnabled);
        AppSettings.TokenJwtExpiration = int.Parse(tokenJwtExpiration);
        AppSettings.IsUpperCaseText = bool.Parse(isUpperCaseText);
        AppSettings.UrlLxConnector = CryptoHelper.Decrypt(urlLxConnector);

        AppSettings.Connection.DefaultConnection = CryptoHelper.Decrypt(defaultConnection);
        AppSettings.Connection.Hangfire = CryptoHelper.Decrypt(hangfire);
        AppSettings.Connection.DB2 = CryptoHelper.Decrypt(DB2);
        AppSettings.Connection.FPW = CryptoHelper.Decrypt(FPW);
        AppSettings.Connection.InfoteutoWeb = CryptoHelper.Decrypt(InfoteutoWeb);
        AppSettings.Connection.DTI0016 = CryptoHelper.Decrypt(DTI0016);
        AppSettings.Connection.IsoWeb = CryptoHelper.Decrypt(IsoWeb);
        AppSettings.Connection.IsoWebIntegracao = CryptoHelper.Decrypt(IsoWebIntegracao);
        AppSettings.Connection.Evolutio = CryptoHelper.Decrypt(Evolutio);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public virtual AppSettings GetAppSettings()
    {
        return AppSettings;
    }
}
