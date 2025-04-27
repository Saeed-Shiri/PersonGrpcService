

using MediatR;
using PersonSrv.Application.Common.Dtos;
using PersonSrv.Domain.Repositories;

namespace PersonSrv.Application.Features.UpdatePerson;

public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, PersonDto>
{
    private readonly IPersonRepository _personRepository;

    public UpdatePersonCommandHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
    }

    public async Task<PersonDto> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.Id);
        if (person == null)
        {
            throw new KeyNotFoundException($"Person with ID {request.Id} not found.");
        }

        person.Update(
            request.Person.FirstName,
            request.Person.LastName,
            request.Person.NationalCode,
            request.Person.BirthDate);

        await _personRepository.UpdateAsync(person);
        await _personRepository.SaveChangesAsync();

        return new PersonDto(
            person.Id,
            person.FirstName,
            person.LastName,
            person.NationalCode,
            person.BirthDate);
    }
}
