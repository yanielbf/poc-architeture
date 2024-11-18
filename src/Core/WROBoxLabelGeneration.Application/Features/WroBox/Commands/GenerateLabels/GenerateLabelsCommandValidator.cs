using FluentValidation;

namespace WROBoxLabelGeneration.Application.Features.WroBox.Commands.GenerateLabels
{
    public class GenerateLabelsCommandValidator : AbstractValidator<GenerateLabelsCommand>
    {
        public GenerateLabelsCommandValidator()
        {
            //RuleFor(p => p.WroId)
            //    .NotNull()
            //    .GreaterThan(0);
        }
    }
}
