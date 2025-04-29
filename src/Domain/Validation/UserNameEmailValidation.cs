using FluentValidation;
using StudentCenterAuthApi.src.Domain.Model;

namespace StudentCenterAuthApi.src.Domain.Validation;

public class UserNameEmailValidation : AbstractValidator<User>
{
    public UserNameEmailValidation()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email não pode ser vazio!");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email inválido!");
        RuleFor(x => x.Email).MinimumLength(12).WithMessage("Email tem que ter no mínimo 12 caracteres");
        RuleFor(x => x.Email).MaximumLength(50).WithMessage("Email tem que ter no máximo 50 caracteres");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Nome não pode ser vazio!");        
        RuleFor(x => x.Name).MinimumLength(2).WithMessage("Nome tem que ter no mínimo 2 caracteres");
        RuleFor(x => x.Name).MaximumLength(100).WithMessage("Nome tem que ter no máximo 100 caracteres");
    }
}
