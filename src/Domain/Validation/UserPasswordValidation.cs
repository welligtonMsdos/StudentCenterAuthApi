using FluentValidation;
using StudentCenterAuthApi.src.Domain.Model;

namespace StudentCenterAuthApi.src.Domain.Validation;

public class UserPasswordValidation : AbstractValidator<User>
{
    public UserPasswordValidation()
    {
        RuleFor(x => x.PassWord).NotEmpty().WithMessage("Senha não pode ser vazio!");
        RuleFor(x => x.PassWord).MinimumLength(6).WithMessage("Senha tem que ter no mínimo 6 caracteres");
    }
}
