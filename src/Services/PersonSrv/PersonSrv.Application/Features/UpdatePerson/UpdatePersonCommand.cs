

using MediatR;
using PersonSrv.Application.Common.Dtos;

namespace PersonSrv.Application.Features.UpdatePerson;

public record UpdatePersonCommand(Guid Id, PersonDto Person) : IRequest<PersonDto>;
