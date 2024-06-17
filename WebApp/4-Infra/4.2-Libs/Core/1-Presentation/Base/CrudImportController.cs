using ArrayToExcel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TInput"></typeparam>
/// <typeparam name="TOutput"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="IService"></typeparam>
/// <typeparam name="IRepository"></typeparam>
/// <param name="responseObject"></param>
/// <param name="appSettings"></param>
/// <param name="service"></param>
public abstract class CrudImportController<TInput, TOutput, TEntity, 
    IService, IRepository>(AppSettings appSettings, ResponseObject responseObject, IService service) : 
    CrudController<TInput, TOutput, TEntity, IService, IRepository>(appSettings, responseObject, service)
    where TInput : class, IBaseInput
    where TOutput : class, IBaseOutput
    where TEntity : class, IBaseAuditEntity
    where IService : class, ICrudService<TInput, TOutput, TEntity, IRepository>
    where IRepository : class, ICrudRepository<TEntity>
{
    /// <summary>
    /// Faz o download do layout de inserção
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetDeleteLayout")]
    public virtual IActionResult GetDeleteLayout()
    {
        var fileBytes = ExcelHelper.GetLayout<BaseImportInput>(true, "Excluir");

        var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        var fileName = $"layout-inserir.xlsx";

        return File(fileBytes, contentType: contentType, fileName);
    }

    /// <summary>
    /// Faz o download do layout de inserção
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetInsertLayout")]
    public virtual IActionResult GetInsertLayout()
    {
        var fileBytes = ExcelHelper.GetLayout<TInput>(false, "Inserir");

        var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        var fileName = $"layout-inserir.xlsx";

        return File(fileBytes, contentType, fileName);
    }

    /// <summary>
    /// Faz o download do layout de atualização
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetUpdateLayout")]
    public virtual IActionResult GetUpdateLayout()
    {
        var fileContents = ExcelHelper.GetLayout<TInput>(true, "Atualizar");

        var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        var fileDownloadName = $"layout-atualizar.xlsx";

        return File(fileContents, contentType: contentType, fileDownloadName);
    }

    /// <summary>
    /// Insere registros por uma planilha de excel
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPost("InsertByImport")]
    public virtual IActionResult InsertByImport(IFormFile file)
    {
        if (file.Length == 0)
            return BadRequest("Nenhum arquivo foi enviado.");

        var fileContents = _service.Insert(file);
        var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        return File(fileContents, contentType, "log-retorno-inserir.xlsx");
    }

    /// <summary>
    /// Insere registros por uma planilha de excel
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPost("UpdateByImport")]
    public virtual IActionResult UpdateByImport(IFormFile file)
    {
        if (file.Length == 0)
            return BadRequest("Nenhum arquivo foi enviado.");

        var fileContents = _service.Update(file);
        var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        return File(fileContents, contentType, "log-retorno-atualizar.xlsx");
    }

    [HttpPost("DeleteByImport")]
    public virtual IActionResult DeleteByImport(IFormFile file)
    {
        if (file.Length == 0)
            return BadRequest("Nenhum arquivo foi enviado.");

        var fileContents = _service.Delete(file);
        var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        return File(fileContents, contentType, "log-retorno-excluir.xlsx");
    }
}
