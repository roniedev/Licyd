using FluentValidation;

namespace BaseLib;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TRepository"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public class InactivateValidator<TRepository, TEntity> : AbstractValidator<InactivateInput>
    where TRepository : ICrudRepository<TEntity>
    where TEntity : IBaseStatusEntity
{
    private TEntity _entity;
    private readonly TRepository _repository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository"></param>
    public InactivateValidator(TRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Id)
            .Must(id => id.HasValue)
            .WithMessage("O campo Id é obrigatório.");

        RuleFor(x => x.Id.Value)
            .Must(IsEntityExists)
            .When(x => x.Id.HasValue)
            .WithMessage("O registro informado não foi encontrado.");

        RuleFor(x => x.ReasonInactivation)
            .Must(motivo => string.IsNullOrEmpty(motivo))
            .WithMessage("O campo Motivo de Inativação é obrigatório.");

        #region Tipo entidade

        RuleFor(x => x.Id.Value)
            .Must(IsEntityValid)
            .When(x => _entity != null)
            .WithMessage("O tipo da entidade é inválida.");

        RuleFor(x => x.Id.Value)
            .Must(IsStatusValid)
            .When(x => _entity != null)
            .WithMessage("O registro informado não está ativo.");

        #endregion
    }

    private bool IsEntityExists(long id)
    {
        _entity = _repository is ICrudEfRepository<TEntity>
            ? (_repository as ICrudEfRepository<TEntity>).GetById(id)
            : (_repository as ICrudOdbcRepository<TEntity>).GetById(id);

        return _entity != null;
    }

    private bool IsEntityValid(long id)
    {
        return _entity is IBaseStatusEntity;
    }

    private bool IsStatusValid(long id)
    {
        return _entity.StatusId == StatusEnum.Active;
    }
}
