using FluentValidation;
using GovHospitalApp.Core.Application.Infrastructure.Hospitals.Commands;

namespace GovHospitalApp.Core.Application.Infrastructure.Hospitals.Validators
{
    public class SaveHospitalCommandValidator : AbstractValidator<SaveHospital.Command>
    {
        public SaveHospitalCommandValidator()
        {
            RuleFor(X => X.Name)
            .NotEmpty()
            .MaximumLength(255);
            RuleFor(x => x.MobileNumber)
            .NotEmpty()
            .MaximumLength(12);
            RuleFor(x => x.Address)
            .NotNull();
        }
    }
}
