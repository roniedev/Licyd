using Microsoft.AspNetCore.Mvc;

namespace BaseLib;


public class CrudController<TInput, TOutput, TEntity, TService,
    TRepository>(AppSettings appSettings, ResponseObject responseObject, TService service) :
    BaseController(appSettings, responseObject)
    where TInput : class, IBaseInput
    where TOutput : class, IBaseOutput
    where TEntity : class, IBaseAuditEntity
    where TService : class, ICrudService<TInput, TOutput, TEntity, TRepository>
    where TRepository : class, ICrudRepository<TEntity>
{
    /// <summary>
    /// Nome do arquivo ao exportar
    /// </summary>
    protected readonly string _fileDownloadName = "exportacao.xlsx";

    /// <summary>
    /// Instancia do service
    /// </summary>
    protected readonly TService _service = service;

    /// <summary>
    /// Apaga o registro.
    /// </summary>
    /// <param name="id">Identificador do registro.</param>
    /// <response code="200">Processado com sucesso.</response>
    /// <response code="400">Erro na validação dos dados.</response>
    /// <response code="401">Usuário não autorizado.</response>
    /// <response code="415">Tipo de mídia não suportado.</response>
    /// <response code="500">Erro interno na aplicação.</response>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public virtual IActionResult Delete(long id)
    {
        _service.Delete(id);
        return Ok();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filtros"></param>
    /// <returns></returns>
    [HttpPost("ExportToExcel")]
    public virtual async Task<IActionResult> ExportToExcel([FromBody] List<Filter> filtros)
    {
        var fileContents = await Task.FromResult(_service.Export(filtros));
        var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        return File(fileContents, contentType, _fileDownloadName);
    }

    /// <summary>
    /// Insere um registro.
    /// </summary>
    /// <param name="viewModel">Objeto do tipo BaseViewModel</param>
    /// <response code="200">Processado com sucesso.</response>
    /// <response code="400">Erro na validação dos dados.</response>
    /// <response code="401">Usuário não autorizado.</response>
    /// <response code="415">Tipo de mídia não suportado.</response>
    /// <response code="500">Erro interno na aplicação.</response>
    /// <returns>O objeto que foi criado.</returns>
    [HttpPost]
    public virtual IActionResult Insert([FromBody] TInput viewModel)
    {
        var item = _service.Insert(viewModel);
        return Ok(item);
    }

    /// <summary>
    /// Obtem uma lista de registros paginados.
    /// </summary>
    /// <param name="filtros">Objeto de controle da listagem dos registros.</param>
    /// <response code="200">Processado com sucesso.</response>
    /// <response code="400">Erro na validação dos dados.</response>
    /// <response code="401">Usuário não autorizado.</response>
    /// <response code="415">Tipo de mídia não suportado.</response>
    /// <response code="500">Erro interno na aplicação.</response>
    /// <returns>Lista dos objetos que foram listado.</returns>
    [HttpPost("GetAll")]
    public virtual async Task<IActionResult> GetAll([FromBody] List<Filter> filtros)
    {
        var registros = await Task.FromResult(_service.GetAll(filtros));
        return Ok(registros);
    }

    /// <summary>
    /// Obtem uma lista de registros paginados.
    /// </summary>
    /// <param name="paginacao">Objeto de controle da listagem dos registros.</param>
    /// <response code="200">Processado com sucesso.</response>
    /// <response code="400">Erro na validação dos dados.</response>
    /// <response code="401">Usuário não autorizado.</response>
    /// <response code="415">Tipo de mídia não suportado.</response>
    /// <response code="500">Erro interno na aplicação.</response>
    /// <returns>Lista dos objetos que foram listado.</returns>
    [HttpPost]
    [Route("GetAllPaginated")]
    public virtual async Task<IActionResult> GetAllPaginated([FromBody] GetAllInput paginacao)
    {
        var viewModel = await Task.FromResult(_service.GetAllPaginated(paginacao));
        return Ok(viewModel);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpGet("GetAllSelect")]
    public virtual ActionResult GetAllSelect([FromQuery] string filter)
    {
        return Ok(_service.GetAll(filter));
    }

    /// <summary>
    /// Obtem o registro pelo Id.
    /// </summary>
    /// <param name="id">Identificador do registro.</param>
    /// <response code="200">Processado com sucesso.</response>
    /// <response code="400">Erro na validação dos dados.</response>
    /// <response code="401">Usuário não autorizado.</response>
    /// <response code="415">Tipo de mídia não suportado.</response>
    /// <response code="500">Erro interno na aplicação.</response>
    /// <returns>O objeto que foi listado.</returns>
    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetById(long id)
    {
        var item = await Task.FromResult(_service.GetById(id));
        return Ok(item);
    }

    /// <summary>
    /// Atualiza um registro.
    /// </summary>
    /// <param name="viewModel">Objeto que será atualizado.</param>
    /// <response code="200">Processado com sucesso.</response>
    /// <response code="400">Erro na validação dos dados.</response>
    /// <response code="401">Usuário não autorizado.</response>
    /// <response code="415">Tipo de mídia não suportado.</response>
    /// <response code="500">Erro interno na aplicação.</response>
    /// <returns>O objeto que foi atualizado.</returns>
    [HttpPut]
    public virtual async Task<IActionResult> Update([FromBody] TInput viewModel)
    {
        var item = await Task.FromResult(_service.Update(viewModel));
        return Ok(item);
    }
}
