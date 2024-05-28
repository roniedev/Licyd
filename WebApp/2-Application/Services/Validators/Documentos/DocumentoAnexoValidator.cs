using FluentValidation;

namespace Services;

public class DocumentoAnexoValidator : AbstractValidator<DocumentoAnexoInput>
{
    public DocumentoAnexoValidator()
    {
        RuleFor(x => x.MimeType).Must(mimeType => string.IsNullOrEmpty(mimeType)).WithMessage("O campo MimeType é obrigatório.");
        RuleFor(x => x.MimeType).MaximumLength(128);
    }
}
