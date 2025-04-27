

using MediatR;
using PersonSrv.Application.Common.Dtos;
using PersonSrv.Domain.Entities;
using PersonSrv.Domain.Repositories;

namespace PersonSrv.Application.Features.CreatePerson;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, PersonDto>
{
    private readonly IPersonRepository _personRepository;

    public CreatePersonCommandHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
    }

    public async Task<PersonDto> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = Person.Create(
            request.Person.FirstName,
            request.Person.LastName,
            request.Person.NationalCode,
            request.Person.BirthDate);

        await _personRepository.AddAsync(person);
        await _personRepository.SaveChangesAsync();

        //we can use mapper tools to map the entity to dto
        return new PersonDto(
            person.Id,
            person.FirstName,
            person.LastName,
            person.NationalCode,
            person.BirthDate);
    }
}