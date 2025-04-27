

using FluentValidation;

namespace PersonSrv.Application.Features.GetPersonById;

public class GetPersonByIdQueryValidator : AbstractValidator<GetPersonByIdQuery>
{
    public GetPersonByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Person ID is required.");
    }
}
