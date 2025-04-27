

using FluentValidation;

namespace PersonSrv.Application.Features.GetAllPersons;

public class GetAllPersonsQueryValidator : AbstractValidator<GetAllPersonsQuery>
{
    public GetAllPersonsQueryValidator()
    {
        // No validations needed for GetAllPersonsQuery
    }
}
