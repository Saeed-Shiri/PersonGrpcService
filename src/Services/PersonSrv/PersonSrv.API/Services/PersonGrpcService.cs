using Grpc.Core;
using Person.Grpc.Protos;


namespace PersonSrv.API.Services;

public class PersonGrpcService : PersonService.PersonServiceBase
{
    public PersonGrpcService()
    {
        
    }

    public override Task<PersonResponse> CreatePerson(CreatePersonRequest request, ServerCallContext context)
    {
        return base.CreatePerson(request, context);
    }
}
