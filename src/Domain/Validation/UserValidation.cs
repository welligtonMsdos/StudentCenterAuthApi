using FluentValidation;
using StudentCenterAuthApi.src.Domain.Model;

namespace StudentCenterAuthApi.src.Domain.Validation;

public class UserValidation : AbstractValidator<User>
{
    public UserValidation()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email não pode ser vazio!");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email inválido!");
        RuleFor(x => x.Email).MinimumLength(12).WithMessage("Email tem que ter no mínimo 12 caracteres");
        RuleFor(x => x.Email).MaximumLength(50).WithMessage("Email tem que ter no máximo 50 caracteres");

        RuleFor(x => x.PassWord).NotEmpty().WithMessage("Senha não pode ser vazio!");
        RuleFor(x => x.PassWord).MinimumLength(6).WithMessage("Senha tem que ter no mínimo 6 caracteres");
    }
}
