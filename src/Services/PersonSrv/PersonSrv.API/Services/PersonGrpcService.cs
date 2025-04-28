using Grpc.Core;
using MediatR;
using Person.Grpc.Protos;
using PersonSrv.Application.Common.Dtos;
using PersonSrv.Application.Features.CreatePerson;
using PersonSrv.Application.Features.DeletePerson;
using PersonSrv.Application.Features.GetAllPersons;
using PersonSrv.Application.Features.GetPersonById;
using PersonSrv.Application.Features.UpdatePerson;
using Google.Protobuf.WellKnownTypes;



namespace PersonSrv.API.Services;

public class PersonGrpcService : PersonService.PersonServiceBase
{
    private readonly IMediator _mediator;

    public PersonGrpcService(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public override async Task<PersonResponse> CreatePerson(CreatePersonRequest request, ServerCallContext context)
    {
        var command = new CreatePersonCommand(new PersonDto(
            Guid.Empty,
            request.Person.FirstName,
            request.Person.LastName,
            request.Person.NationalCode,
            request.Person.BirthDate.ToDateTime()));

        var result = await _mediator.Send(command, context.CancellationToken);

        return new PersonResponse
        {
            Person = new Person.Grpc.Protos.Person
            {
                Id = result.Id.ToString(),
                FirstName = result.FirstName,
                LastName = result.LastName,
                NationalCode = result.NationalCode,
                BirthDate = Timestamp.FromDateTime(result.BirthDate.ToUniversalTime())
            }
        };
    }

    public override async Task<PersonResponse> GetPerson(GetPersonRequest request, ServerCallContext context)
    {
        var query = new GetPersonByIdQuery(Guid.Parse(request.Id));
        var result = await _mediator.Send(query, context.CancellationToken);

        return new PersonResponse
        {
            Person = new Person.Grpc.Protos.Person
            {
                Id = result.Id.ToString(),
                FirstName = result.FirstName,
                LastName = result.LastName,
                NationalCode = result.NationalCode,
                BirthDate = Timestamp.FromDateTime(result.BirthDate.ToUniversalTime())
            }
        };
    }

    public override async Task<PersonListResponse> GetAllPersons(Empty request, ServerCallContext context)
    {
        var query = new GetAllPersonsQuery();
        var results = await _mediator.Send(query, context.CancellationToken);

        var response = new PersonListResponse();
        response.Persons.AddRange(results.Select(x => new Person.Grpc.Protos.Person
        {
            Id = x.Id.ToString(),
            FirstName = x.FirstName,
            LastName = x.LastName,
            NationalCode = x.NationalCode,
            BirthDate = Timestamp.FromDateTime(x.BirthDate.ToUniversalTime())
        }));

        return response;
    }

    public override async Task<PersonResponse> UpdatePerson(UpdatePersonRequest request, ServerCallContext context)
    {
        var command = new UpdatePersonCommand(
            Guid.Parse(request.Person.Id),
            new PersonDto(
                Guid.Parse(request.Person.Id),
                request.Person.FirstName,
                request.Person.LastName,
                request.Person.NationalCode,
                request.Person.BirthDate.ToDateTime()));

        var result = await _mediator.Send(command, context.CancellationToken);

        return new PersonResponse
        {
            Person = new Person.Grpc.Protos.Person
            {
                Id = result.Id.ToString(),
                FirstName = result.FirstName,
                LastName = result.LastName,
                NationalCode = result.NationalCode,
                BirthDate = Timestamp.FromDateTime(result.BirthDate.ToUniversalTime())
            }
        };
    }

    public override async Task<Empty> DeletePerson(DeletePersonRequest request, ServerCallContext context)
    {
        var command = new DeletePersonCommand(Guid.Parse(request.Id));
        await _mediator.Send(command, context.CancellationToken);

        return new Empty();
    }
}
