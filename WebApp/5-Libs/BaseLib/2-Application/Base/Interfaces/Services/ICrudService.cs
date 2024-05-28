using Microsoft.AspNetCore.Http;

namespace BaseLib;

public interface ICrudService<TInput, TOutput, TEntity, TRepository>
    where TInput : class, IBaseInput
    where TOutput : class, IBaseOutput
    where TEntity : class, IBaseAuditEntity
    where TRepository : class, ICrudRepository<TEntity>
{
    void Activate(long id);

    byte[] Activate(IFormFile file);

    void Inactivate(InactivateInput viewModel);

    byte[] Inactivate(IFormFile file);

    void Delete(long id);

    byte[] Delete(IFormFile file);

    byte[] Export(List<Filter> filters);

    List<TOutput> GetAll(List<Filter> filters);

    List<TOutput> GetAll(string filter);

    GetAllOutput<TOutput> GetAllPaginated(GetAllInput viewModel);

    TOutput GetById(long id);

    TOutput Insert(TInput viewModel);

    byte[] Insert(IFormFile file);

    TOutput Update(TInput viewModel, bool notEditActiveEntity = true);

    byte[] Update(IFormFile file, bool editActiveRows = false);
}
