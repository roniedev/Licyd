using Microsoft.AspNetCore.Http;

namespace BaseLib;

/// <summary>
/// Base service
/// </summary>
/// <param name="appSettings"></param>
/// <param name="responseObject"></param>
public abstract class BaseService(AppSettings appSettings, ResponseObject responseObject)
{
    /// <summary>
    /// Classe com os dados do arquivo de configuração
    /// </summary>
    protected readonly AppSettings _appSettings = appSettings;

    /// <summary>
    /// Objeto que irá receber as notificações durante a execução
    /// </summary>
    protected readonly ResponseObject _responseObject = responseObject;

    /// <summary>
    /// Faz a conversão de um Excel para lista
    /// </summary>
    /// <typeparam name="T">Tipo da conversão</typeparam>
    /// <param name="file">Arquivo Excel enviado</param>
    /// <returns>Lista do tipo T</returns>
    /// <exception cref="Exception">A planilha enviada não contém dados ou T não é do tipo IBaseImportViewModel</exception>
    protected virtual List<T> ConvertExcelToList<T>(IFormFile file) where T : class
    {
        var inputs = ExcelHelper.ExcelToList<T>(file);

        if (inputs.Count == 0)
        {
            throw new Exception("A planilha enviada não contém dados!");
        }

        if (inputs.FirstOrDefault() is not IBaseImportInput)
        {
            throw new Exception("A entidade não suporta importações de dados! Favor entrar em contato com o suporte.");
        }

        return inputs;
    }
}