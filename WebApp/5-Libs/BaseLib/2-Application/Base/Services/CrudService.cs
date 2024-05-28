using ArrayToExcel;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using FluentValidation;
using FluentValidation.Results;
using Mapster;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BaseLib;

public class CrudService<TInput, TOutput, TEntity, TRepository>(AppSettings appSettings, ResponseObject responseObject,
    IValidator<TInput> validator, TRepository repository) : BaseService(appSettings, responseObject),
    ICrudService<TInput, TOutput, TEntity, TRepository>
    where TInput : class, IBaseInput
    where TOutput : class, IBaseOutput
    where TEntity : class, IBaseAuditEntity
    where TRepository : class, ICrudRepository<TEntity>
{
    protected readonly TRepository _repository = repository;
    protected readonly IValidator<TInput> _validator = validator;

    public virtual void Delete(long id)
    {
        var (isValid, notifications) = DeleteValidate(id);

        if (isValid)
        {
            _repository.Delete(id);

            if (_repository is ICrudEfRepository<TEntity>)
            {
                (_repository as ICrudEfRepository<TEntity>).Commit();
            }
        }
        else
        {
            _responseObject.StatusCode = HttpStatusCode.BadRequest;
            _responseObject.AddNotifications(notifications);
        }
    }

    public virtual byte[] Delete(IFormFile file)
    {
        var inputs = ConvertExcelToList<BaseImportInput>(file);

        foreach (var input in inputs)
        {
            try
            {
                var (isValid, notifications) = DeleteValidate(input.Id);

                if (isValid)
                {
                    var id = input.Id.Value;

                    _repository.Delete(input.Id.Value);

                    Commit();

                    input.IsSuccess = true;
                    input.Message = "Registro excluído com sucesso.";
                }
                else
                {
                    input.IsSuccess = false;
                    input.Message = ConvertNotificationsToString(notifications);
                }
            }
            catch (Exception ex)
            {
                input.IsSuccess = false;
                input.Message = ex.Message;
            }
        }

        return SetColumnsToExport(inputs);
    }

    public virtual byte[] Export(List<Filter> filters)
    {
        var itens = GetAll(filters);
        var bytes = SetColumnsToExport(itens.Adapt<List<TOutput>>());
        return bytes;
    }

    public virtual List<TOutput> GetAll(List<Filter> filters)
    {
        var itens = _repository.GetAll(filters);
        return itens.Adapt<List<TOutput>>();
    }

    public virtual List<TOutput> GetAll(string filter)
    {
        var itens = _repository.GetAll(filter);
        return itens.Adapt<List<TOutput>>();
    }

    public virtual GetAllOutput<TOutput> GetAllPaginated(GetAllInput viewModel)
    {
        var (rows, totalRows) = _repository.GetAllPaginated(viewModel);

        viewModel.Take = viewModel.Take > 0 ? viewModel.Take : 12;

        var totalPages = totalRows > 0
            ? totalRows < viewModel.Take ? 1
            : totalRows / viewModel.Take + (totalRows % viewModel.Take > 0 ? 1 : 0)
            : 0;

        return new GetAllOutput<TOutput>
        {
            Rows = rows.Skip(viewModel.Skip).Take(viewModel.Take).Adapt<List<TOutput>>(),
            TotalRows = totalRows,
            TotalPages = totalPages,
        };
    }

    public virtual TOutput GetById(long id)
    {
        var itemById = _repository is ICrudEfRepository<TEntity>
            ? (_repository as ICrudEfRepository<TEntity>).GetById(id)
            : (_repository as ICrudOdbcRepository<TEntity>).GetById(id);

        if (itemById == null)
        {
            _responseObject.StatusCode = HttpStatusCode.BadRequest;
            _responseObject.AddNotification("O registro informado não foi encontrado.", false);

            return default;
        }

        return itemById.Adapt<TOutput>();
    }

    public virtual TOutput Insert(TInput viewModel)
    {
        var validationResult = _validator.Validate(viewModel);

        if (validationResult.IsValid)
        {
            var entity = MapToEntity(viewModel);

            entity = _repository.Insert(entity);

            if (_repository is ICrudEfRepository<TEntity>)
            {
                (_repository as ICrudEfRepository<TEntity>).Commit();
            }

            var outputViewModel = entity.Adapt<TOutput>();

            return outputViewModel;
        }

        _responseObject.StatusCode = HttpStatusCode.BadRequest;
        _responseObject.AddNotifications(validationResult);

        return default;
    }

    public virtual byte[] Insert(IFormFile file)
    {
        var inputs = ConvertExcelToList<TInput>(file);

        ImportValidate(inputs.FirstOrDefault());

        foreach (var input in inputs)
        {
            try
            {
                var validationResult = _validator.Validate(input);

                if (validationResult.IsValid)
                {
                    _repository.Insert(MapToEntity(input));

                    (input as IBaseImportInput).IsSuccess = true;
                    (input as IBaseImportInput).Message = "Registro inserido com sucesso!";
                }
                else
                {
                    var notifications = GetErrorsValidationResult(validationResult);

                    (input as IBaseImportInput).IsSuccess = false;
                    (input as IBaseImportInput).Message = ConvertNotificationsToString(notifications);
                }
            }
            catch (Exception ex)
            {
                (input as IBaseImportInput).IsSuccess = false;
                (input as IBaseImportInput).Message = ex.Message;
            }
        }

        return SetColumnsToExport(inputs);
    }

    public virtual TOutput Update(TInput viewModel, bool notEditActiveEntity = true)
    {
        UpdateValidate(viewModel, notEditActiveEntity);

        if (_responseObject.HasNotifications) return default;

        var validationResult = _validator.Validate(viewModel);

        if (validationResult.IsValid)
        {
            var entity = MapToEntity(viewModel, false);

            if (entity is IBaseStatusEntity)
            {
                var id = viewModel.Id.Value;

                var item = _repository is ICrudEfRepository<TEntity>
                    ? (_repository as ICrudEfRepository<TEntity>).GetById(id)
                    : (_repository as ICrudOdbcRepository<TEntity>).GetById(id);

                (entity as IBaseStatusEntity).StatusId = (item as IBaseStatusEntity).StatusId;
            }

            entity = _repository.Update(entity);

            if (_repository is ICrudEfRepository<TEntity>)
            {
                (_repository as ICrudEfRepository<TEntity>).Commit();
            }

            return entity.Adapt<TOutput>();
        }

        _responseObject.StatusCode = HttpStatusCode.BadRequest;
        _responseObject.AddNotifications(validationResult);

        return default;
    }

    public virtual byte[] Update(IFormFile file, bool editActiveRows = false)
    {
        var inputs = ConvertExcelToList<TInput>(file);

        ImportValidate(inputs.FirstOrDefault());

        foreach (var input in inputs)
        {
            try
            {
                var validationResult = _validator.Validate(input);

                if (validationResult.IsValid)
                {
                    _repository.Update(MapToEntity(input));

                    (input as IBaseImportInput).IsSuccess = true;
                    (input as IBaseImportInput).Message = "Registro atualizado com sucesso!";
                }
                else
                {
                    var notifications = GetErrorsValidationResult(validationResult);

                    (input as IBaseImportInput).IsSuccess = false;
                    (input as IBaseImportInput).Message = ConvertNotificationsToString(notifications);
                }
            }
            catch (Exception ex)
            {
                (input as IBaseImportInput).IsSuccess = false;
                (input as IBaseImportInput).Message = ex.Message;
            }
        }

        return SetColumnsToExport(inputs);
    }

    protected virtual void UpdateValidate(TInput viewModel, bool notEditActiveEntity = true)
    {
        if (viewModel.Id == 0)
        {
            _responseObject.AddNotification("O campo id é obrigatório.", false);
            _responseObject.StatusCode = HttpStatusCode.BadRequest;

            return;
        }

        var id = viewModel.Id.Value;

        var entity = _repository is ICrudEfRepository<TEntity>
            ? (_repository as ICrudEfRepository<TEntity>).GetById(id)
            : (_repository as ICrudOdbcRepository<TEntity>).GetById(id);

        if (entity == null)
        {
            _responseObject.AddNotification("O registro informado não foi encontrado.", false);
            _responseObject.StatusCode = HttpStatusCode.BadRequest;
        }
        else
        {
            if (notEditActiveEntity)
            {
                if (entity is IBaseStatusEntity)
                {
                    var statusId = (entity as IBaseStatusEntity).StatusId;

                    if (statusId != StatusEnum.Build && statusId != StatusEnum.BuildClone)
                    {
                        _responseObject.AddNotification("O registro não está em construção, não é possível editar.", false);
                        _responseObject.StatusCode = HttpStatusCode.BadRequest;
                    }
                }
            }
        }
    }

    protected virtual (bool isValid, List<Notification> notifications) DeleteValidate(long? id)
    {
        var notifications = new List<Notification>();

        if (id != null)
        {
            var entity = _repository is ICrudEfRepository<TEntity>
                ? (_repository as ICrudEfRepository<TEntity>).GetById(id.Value)
                : (_repository as ICrudOdbcRepository<TEntity>).GetById(id.Value);

            if (entity == null)
            {
                notifications.Add(new Notification { Message = "O registro informado não existe.", Success = false });
            }
            else if (entity is IBaseStatusEntity)
            {
                if ((entity as IBaseStatusEntity).StatusId != StatusEnum.Build)
                {
                    notifications.Add(new Notification { Message = "O registro informado não está em construção, não é possível excluir.", Success = false });
                }
            }
        }
        else
        {
            notifications.Add(new Notification { Message = "O identificador do registro é obrigatório! Entrar em contato com o suporte.", Success = false });
        }

        return (notifications.Count == 0, notifications);
    }

    protected virtual TEntity MapToEntity(TInput viewModel, bool isCreate = true)
    {
        return viewModel.Adapt<TEntity>();
    }

    protected virtual TOutput MapToOutput(TEntity entity)
    {
        return entity.Adapt<TOutput>();
    }

    protected virtual void ImportValidate(TInput viewModel)
    {
        if (viewModel is not IBaseImportInput)
        {
            throw new Exception("O tipo do objeto não é valido para essa operação.");
        }
    }

    public virtual void Activate(long id)
    {
        var (isValid, notifications) = ActivateValidate(id);

        if (isValid)
        {
            _repository.Activate(id);
            Commit();
        }
        else
        {
            _responseObject.StatusCode = HttpStatusCode.BadRequest;
            _responseObject.AddNotifications(notifications);
        }
    }

    public virtual byte[] Activate(IFormFile file)
    {
        var inputs = ConvertExcelToList<BaseImportInput>(file);

        foreach (var input in inputs)
        {
            try
            {
                var (isValid, notifications) = ActivateValidate(input.Id);

                if (isValid)
                {
                    var id = input.Id.Value;

                    _repository.Activate(input.Id.Value);

                    Commit();

                    input.IsSuccess = true;
                    input.Message = "Registro ativado com sucesso.";
                }
                else
                {
                    input.IsSuccess = false;
                    input.Message = ConvertNotificationsToString(notifications);
                }
            }
            catch (Exception ex)
            {
                input.IsSuccess = false;
                input.Message = ex.Message;
            }
        }

        return SetColumnsToExport(inputs);
    }

    public virtual void Inactivate(InactivateInput viewModel)
    {
        var (result, notifications) = InactivateValidate(viewModel);

        if (result)
        {
            var id = viewModel.Id.Value;
            var reasonInactivation = viewModel.ReasonInactivation;

            _repository.Inactivate(id, reasonInactivation);
            Commit();
        }
        else
        {
            _responseObject.StatusCode = HttpStatusCode.BadRequest;
            _responseObject.AddNotifications(notifications);
        }
    }

    public virtual byte[] Inactivate(IFormFile file)
    {
        var inputs = ConvertExcelToList<InactivateInput>(file);

        foreach (var input in inputs)
        {
            try
            {
                var (isValid, notifications) = InactivateValidate(input);

                if (isValid)
                {
                    var id = input.Id.Value;
                    var reasonInactivation = input.ReasonInactivation;

                    _repository.Inactivate(id, reasonInactivation);

                    Commit();

                    input.IsSuccess = true;
                    input.Message = "Registro inativado com sucesso.";
                }
                else
                {
                    input.IsSuccess = false;
                    input.Message = ConvertNotificationsToString(notifications);
                }
            }
            catch (Exception ex)
            {
                input.IsSuccess = false;
                input.Message = ex.Message;
            }
        }

        return SetColumnsToExport(inputs);
    }

    protected virtual (bool isValid, List<Notification> notifications) ActivateValidate(long? id)
    {
        var notifications = new List<Notification>();

        if (id != null)
        {
            var entity = _repository is ICrudEfRepository<TEntity>
                ? (_repository as ICrudEfRepository<TEntity>).GetById(id.Value)
                : (_repository as ICrudOdbcRepository<TEntity>).GetById(id.Value);

            if (entity == null)
            {
                notifications.Add(new Notification { Message = "O registro informado não existe.", Success = false });
            }
            else
            {
                if (entity is IBaseStatusEntity)
                {
                    if ((entity as IBaseStatusEntity).StatusId == StatusEnum.Active)
                    {
                        notifications.Add(new Notification { Message = "O registro informado já está ativo.", Success = false });
                    }
                }
                else
                {
                    notifications.Add(new Notification { Message = "O tipo da entidade é inválido! Entrar em contato com o suporte.", Success = false });
                }
            }
        }
        else
        {
            notifications.Add(new Notification { Message = "O identificador do registro é obrigatório! Entrar em contato com o suporte.", Success = false });
        }

        return (notifications.Count == 0, notifications);
    }

    protected virtual (bool result, List<Notification> notifications) InactivateValidate(InactivateInput viewModel)
    {
        var notifications = new List<Notification>();

        if (!viewModel.Id.HasValue)
        {
            notifications.Add(new Notification { Message = "O campo Id é obrigatório." });
        }
        else
        {
            var id = viewModel.Id.Value;

            var item = _repository is ICrudEfRepository<TEntity>
                ? (_repository as ICrudEfRepository<TEntity>).GetById(id)
                : (_repository as ICrudOdbcRepository<TEntity>).GetById(id);

            if (item == null)
            {
                notifications.Add(new Notification { Message = "O registro informado não foi encontrado." });
            }
            else
            {
                if (item is not IBaseStatusEntity)
                {
                    notifications.Add(new Notification { Message = "O tipo da entidade é inválido! Entrar em contato com o suporte." });
                }
                else
                {
                    if ((item as IBaseStatusEntity).StatusId != StatusEnum.Active)
                    {
                        notifications.Add(new Notification { Message = "O registro informado não está ativo." });
                    }
                }
            }
        }

        if (string.IsNullOrEmpty(viewModel.ReasonInactivation))
        {
            notifications.Add(new Notification { Message = "O registro informado não foi encontrado." });
        }

        return (notifications.Count == 0, notifications);
    }

    protected static List<Notification> GetErrorsValidationResult(ValidationResult validationResult)
    {
        var notifications = new List<Notification>();

        foreach (var error in validationResult.Errors)
        {
            notifications.Add(new Notification { Message = error.ErrorMessage, Success = false });
        }

        return notifications;
    }

    protected static string ConvertNotificationsToString(List<Notification> notifications)
    {
        return string.Join(", ", notifications.Select(x => x.Message));
    }

    protected void Commit()
    {
        if (_repository is ICrudEfRepository<TEntity>)
        {
            (_repository as ICrudEfRepository<TEntity>).Commit();
        }
    }

    protected static byte[] SetColumnsToExport<T>(List<T> itens) where T : class
    {
        var bytes = itens.ToExcel(schema =>
        {
            var properties = typeof(T).GetProperties();

            var orderedProperties = properties
                .Where(property => Attribute.IsDefined(property, typeof(ExcelColumn)))
                .Select(property => new
                {
                    Property = property,
                    Attribute = (ExcelColumn)Attribute.GetCustomAttribute(property, typeof(ExcelColumn))
                }).OrderBy(p => p.Attribute?.Order ?? int.MaxValue).ToList();

            foreach (var item in orderedProperties)
            {
                var property = item.Property;
                var attribute = item.Attribute;

                if (property.PropertyType == typeof(bool))
                {
                    schema.AddColumn(attribute.Name, item => (bool)property.GetValue(item) ? "Sim" : "Não", attribute.Width);
                }
                else if (property.PropertyType == typeof(DateTime))
                {
                    schema.AddColumn(attribute.Name, item => ((DateTime)property.GetValue(item)).ToString("dd-MM-yyyy HH:mm:ss"), attribute.Width);
                }
                else
                {
                    schema.AddColumn(attribute.Name, item => property.GetValue(item), attribute.Width);
                }
            }
        });

        return bytes;
    }
}