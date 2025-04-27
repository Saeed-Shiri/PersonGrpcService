

using MediatR;
using PersonSrv.Application.Common.Dtos;
using PersonSrv.Domain.Repositories;

namespace PersonSrv.Application.Features.GetAllPersons;

public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, IEnumerable<PersonDto>>
{
    private readonly IPersonRepository _personRepository;

    public GetAllPersonsQueryHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
    }

    public async Task<IEnumerable<PersonDto>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
    {
        var persons = await _personRepository.GetAllAsync();
        return persons.Select(p => new PersonDto(
            p.Id,
            p.FirstName,
            p.LastName,
            p.NationalCode,
            p.BirthDate));
    }
}
