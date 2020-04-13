using FluentValidation;
using GovHospitalApp.Core.Application.Infrastructure.Patients.Commands;

namespace GovHospitalApp.Core.Application.Infrastructure.Hospitals.Validators
{
    public class SavePatientCommandValidator : AbstractValidator<EditPatient.Command>
    {
        public SavePatientCommandValidator()
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
            RuleFor(x => x.Gender)
            .NotNull();
            RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .NotNull();
        }
    }
}
