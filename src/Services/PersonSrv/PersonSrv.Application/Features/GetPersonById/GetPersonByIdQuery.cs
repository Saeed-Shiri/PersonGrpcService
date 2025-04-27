

using MediatR;
using PersonSrv.Application.Common.Dtos;

namespace PersonSrv.Application.Features.GetPersonById;

public record GetPersonByIdQuery(Guid Id) : IRequest<PersonDto>;
