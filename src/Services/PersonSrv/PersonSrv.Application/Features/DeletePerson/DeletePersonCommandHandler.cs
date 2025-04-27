
using MediatR;
using PersonSrv.Domain.Repositories;

namespace PersonSrv.Application.Features.DeletePerson;

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand>
{
    private readonly IPersonRepository _personRepository;

    public DeletePersonCommandHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
    }

    public async Task Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.Id);
        if (person == null)
        {
            throw new KeyNotFoundException($"Person with ID {request.Id} not found.");
        }

        await _personRepository.DeleteAsync(person);
        await _personRepository.SaveChangesAsync();
    }
}
