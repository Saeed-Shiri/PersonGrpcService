

using MediatR;

namespace PersonSrv.Application.Features.DeletePerson;

public record DeletePersonCommand(Guid Id) : IRequest;
