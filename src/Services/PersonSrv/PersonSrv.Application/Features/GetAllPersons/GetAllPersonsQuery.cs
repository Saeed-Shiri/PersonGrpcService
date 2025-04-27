using MediatR;
using PersonSrv.Application.Common.Dtos;

namespace PersonSrv.Application.Features.GetAllPersons;

public record GetAllPersonsQuery : IRequest<IEnumerable<PersonDto>>;