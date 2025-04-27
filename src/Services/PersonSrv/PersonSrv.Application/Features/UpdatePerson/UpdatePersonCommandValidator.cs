

using FluentValidation;

namespace PersonSrv.Application.Features.UpdatePerson;

public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
{
    public UpdatePersonCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Person ID is required.");

        RuleFor(x => x.Person).NotNull().WithMessage("Person data is required.");

        RuleFor(x => x.Person.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

        RuleFor(x => x.Person.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(x => x.Person.NationalCode)
            .NotEmpty().WithMessage("National code is required.")
            .Length(10).WithMessage("National code must be exactly 10 characters.")
            .Matches("^[0-9]+$").WithMessage("National code must contain only digits.");

        RuleFor(x => x.Person.BirthDate)
            .LessThan(DateTime.Today).WithMessage("Birth date must be in the past.");
    }
}