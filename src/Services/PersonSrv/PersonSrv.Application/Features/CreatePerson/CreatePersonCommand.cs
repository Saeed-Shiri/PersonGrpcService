
using MediatR;
using PersonSrv.Application.Common.Dtos;

namespace PersonSrv.Application.Features.CreatePerson;

public record CreatePersonCommand(PersonDto Person) : IRequest<PersonDto>;