using Domain;
using FluentValidation;

namespace Services;

public class DocumentoValidator : AbstractValidator<DocumentoInput>
{
    private readonly IDocumentoRepository _repository;

    public DocumentoValidator(IDocumentoRepository repository)
    {
        RuleFor(x => x.Nome).Must(nome => !string.IsNullOrEmpty(nome)).WithMessage("O campo Nome é obrigatório.");
        RuleFor(x => x.Nome).MaximumLength(128);
        RuleFor(x => x.Observacao).MaximumLength(512);
        RuleFor(x => x).Must(DocumentoNaoExiste).WithMessage(x => $"O nome {x.Nome} já está cadastrado para outro documento.");

        _repository = repository;
    }

    public bool DocumentoNaoExiste(DocumentoInput input)
    {
        var documento = _repository.GetWhere(x => x.Nome == input.Nome);
        return documento == null || documento?.Id == input.Id;
    }
}
