using Application.Patients.Commands;
using FluentValidation;

namespace Application.Patients.Validators
{
    public class EditPatientCommandValidator : AbstractValidator<EditPatient.Command>
    {
        public EditPatientCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
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