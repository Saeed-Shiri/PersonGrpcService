

using MediatR;
using PersonSrv.Application.Common.Dtos;
using PersonSrv.Domain.Repositories;

namespace PersonSrv.Application.Features.GetPersonById;

public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, PersonDto>
{
    private readonly IPersonRepository _personRepository;

    public GetPersonByIdQueryHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
    }

    public async Task<PersonDto> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.Id);
        if (person == null)
        {
            throw new KeyNotFoundException($"Person with ID {request.Id} not found.");
        }
        return new PersonDto(
            person.Id,
            person.FirstName,
            person.LastName,
            person.NationalCode,
            person.BirthDate);
    }
}
