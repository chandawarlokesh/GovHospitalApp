using Application.Hospitals.Commands;
using FluentValidation;

namespace Application.Hospitals.Validators
{
    public class EditHospitalCommandValidator : AbstractValidator<EditHospital.Command>
    {
        public EditHospitalCommandValidator()
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
        }
    }
}