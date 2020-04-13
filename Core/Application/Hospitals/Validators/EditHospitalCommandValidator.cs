using FluentValidation;
using GovHospitalApp.Core.Application.Infrastructure.Hospitals.Commands;

namespace GovHospitalApp.Core.Application.Infrastructure.Hospitals.Validators
{
    public class EditHospitalCommandValidator : AbstractValidator<EditHospital.Command>
    {
        public EditHospitalCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
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
