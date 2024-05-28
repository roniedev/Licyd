#pragma warning disable IDE0290

using Microsoft.AspNetCore.Mvc;

namespace BaseLib;

public class CrudStatusController<TInput, TOutput, TEntity, TService, TRepository> : CrudController<TInput, TOutput, 
        TEntity, TService, TRepository>
    where TInput : class, IBaseInput
    where TOutput : class, IBaseStatusOutput
    where TEntity : class, IBaseStatusEntity
    where TService : class, ICrudService<TInput, TOutput, TEntity, TRepository>
    where TRepository : class, ICrudRepository<TEntity>
{
    public CrudStatusController(AppSettings appSettings, 
        ResponseObject responseObject, TService service) : 
        base(appSettings, responseObject, service)
    {
    }

    /// <summary>
    /// Ativa um registro.
    /// </summary>
    /// <param name="id">Identificador do objeto que será atualizado.</param>
    /// <response code="200">Registro atualizado com sucesso.</response>
    /// <response code="400">Ocorreu um erro de validação de dados.</response>
    /// <response code="401">Usuário não autorizado.</response>
    /// <response code="415">Tipo de mídia não suportado, deve ser enviado em formato JSON.</response>
    /// <response code="500">Retorna o erro de exceção não tratada.</response>
    /// <returns>O objeto que foi atualizado.</returns>
    [HttpPatch("Activate/{id}")]
    public virtual IActionResult Activate(long id)
    {
        _service.Activate(id);
        return Ok();
    }

    /// <summary>
    /// Inativa um registro.
    /// </summary>
    /// <param name="viewModel"></param>
    /// <response code="200">Processado com sucesso.</response>
    /// <response code="400">Erro na validação dos dados.</response>
    /// <response code="401">Usuário não autorizado.</response>
    /// <response code="415">Tipo de mídia não suportado.</response>
    /// <response code="500">Erro interno na aplicação.</response>
    /// <returns>O objeto que foi atualizado.</returns>
    [HttpPatch("Inactivate")]
    public virtual IActionResult Inactivate([FromBody] InactivateInput viewModel)
    {
        _service.Inactivate(viewModel);
        return Ok();
    }
}
