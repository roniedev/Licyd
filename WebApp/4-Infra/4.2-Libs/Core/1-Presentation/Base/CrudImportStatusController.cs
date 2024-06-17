using ArrayToExcel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core
{
    public class CrudImportStatusController<TInput, TOutput, TEntity, IService,
        IRepository>(AppSettings appSettings, ResponseObject responseObject, IService service) :
        CrudImportController<TInput, TOutput, TEntity, IService, IRepository>(appSettings, responseObject, service)
        where TInput : class, IBaseInput
        where TOutput : class, IBaseStatusOutput
        where TEntity : class, IBaseStatusEntity
        where IService : class, ICrudService<TInput, TOutput, TEntity, IRepository>
        where IRepository : class, ICrudRepository<TEntity>
    {
        /// <summary>
        /// Faz o download do layout de inativação
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetActivateLayout")]
        public virtual IActionResult GetActivateLayout()
        {
            var fileBytes = ExcelHelper.GetLayout<BaseImportInput>(true, "Ativar");

            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = $"layout-inativar.xlsx";

            return File(fileBytes, contentType: contentType, fileName);
        }

        /// <summary>
        /// Faz o download do layout de inativação
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetInactivateLayout")]
        public virtual IActionResult GetInactivateLayout()
        {
            var fileBytes = ExcelHelper.GetLayout<InactivateInput>(true, "Inativar");

            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = $"layout-inativar.xlsx";

            return File(fileBytes, contentType: contentType, fileName);
        }

        /// <summary>
        /// Insere registros por uma planilha de excel
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("InactivateByImport")]
        public virtual IActionResult InactivateByImport(IFormFile file)
        {
            if (file.Length == 0)
                return BadRequest("Nenhum arquivo foi enviado.");

            var fileContents = _service.Inactivate(file);
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(fileContents, contentType, "log-retorno-inativar.xlsx");
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
        /// Insere registros por uma planilha de excel
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("ActivateByImport")]
        public virtual IActionResult ActivateByImport(IFormFile file)
        {
            if (file.Length == 0)
                return BadRequest("Nenhum arquivo foi enviado.");

            var fileContents = _service.Activate(file);
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(fileContents, contentType, "log-retorno-ativar.xlsx");
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
}
