using FluentValidation;
using GovHospitalApp.Core.Application.Infrastructure.Patients.Commands;

namespace GovPatientApp.Core.Application.Infrastructure.Patients.Validators
{
    public class SavePatientCommandValidator : AbstractValidator<SavePatient.Command>
    {
        public SavePatientCommandValidator()
        {
            RuleFor(X => X.Name)
            .NotEmpty()
            .MaximumLength(255);
            RuleFor(x => x.MobileNumber)
            .NotEmpty()
            .MaximumLength(12);
            RuleFor(x => x.Address)
            .NotNull();
            RuleFor(x => x.Gender)
            .NotNull();
            RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .NotNull();
        }
    }
}
