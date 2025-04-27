

namespace PersonSrv.Application.Common.Dtos;

public record PersonDto(Guid Id, string FirstName, string LastName, string NationalCode, DateTime BirthDate);