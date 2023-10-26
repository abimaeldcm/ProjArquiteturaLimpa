using CleanArch.Application.ViewModels;
using CleanArch.Domain.Entities;
using FluentValidation;

namespace CleanArch.Application.InputModes
{
    public class ValidatorUser : AbstractValidator<UserMkt>
    {
        public ValidatorUser()
        {
            RuleFor(x => x.Id).NotEmpty()
                .WithMessage("O Id é obrigatório")
                .WithName(c => c.Id.ToString());

            RuleFor(x => x.Username).NotEmpty()
                .WithMessage("O nome do uuário é obrigatória")
                .WithName(c => c.Username.ToString());

            RuleFor(x => x.Password).NotEmpty()
                .WithMessage("A senha é obrigatória")
                .WithName(c => c.Password.ToString());
        }
    }
}