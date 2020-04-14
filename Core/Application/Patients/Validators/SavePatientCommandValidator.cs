using Application.Patients.Commands;
using FluentValidation;

namespace Application.Patients.Validators
{
    public class SavePatientCommandValidator : AbstractValidator<SavePatient.Command>
    {
        public SavePatientCommandValidator()
        {
            RuleFor(x => x.Name)
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