using CleanArch.Application.ViewModels;
using FluentValidation;

namespace CleanArch.Application.InputModes
{
    public class Validator : AbstractValidator<ProductViewModel>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("O nome é obrigatório")
                .MinimumLength(3)
                .MaximumLength(50)
                .WithName(c => c.Name.ToString());

            RuleFor(x => x.Description).NotEmpty()
                .WithMessage("Uma descrição é obrigatória")
                .MinimumLength(5)
                .MaximumLength(500)
                .WithName(c => c.Description.ToString());

            RuleFor(x => x.Price).NotEmpty()
                .WithMessage("O preço é obrigatório")
                .InclusiveBetween(0.1M,999.99M)
                .WithName(c => c.Price.ToString());
        }
    }
}
