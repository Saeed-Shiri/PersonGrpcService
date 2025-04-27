

using FluentValidation;

namespace PersonSrv.Application.Features.DeletePerson;

public class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
{
    public DeletePersonCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Person ID is required.");
    }
}