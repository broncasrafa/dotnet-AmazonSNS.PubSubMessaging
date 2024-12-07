using CloudNotes.Application.DTO.Request;
using FluentValidation;

namespace CloudNotes.Application.Validations.Note;

internal class NoteCreateValidation : AbstractValidator<NoteCreateRequest>
{
    public NoteCreateValidation()
    {
        RuleFor(c => c.Title).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Note title é obrigatório")
            .NotNull().WithMessage("Note title é obrigatório")
            .MinimumLength(3).WithMessage($"Note title deve ter pelo menos 3 caracteres");

        RuleFor(c => c.Content).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Note content é obrigatório")
            .NotNull().WithMessage("Note content é obrigatório")
            .MinimumLength(3).WithMessage($"Note title deve ter pelo menos 3 caracteres");
    }
}
