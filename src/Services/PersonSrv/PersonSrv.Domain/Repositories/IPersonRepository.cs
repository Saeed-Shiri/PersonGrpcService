

using PersonSrv.Domain.Entities;

namespace PersonSrv.Domain.Repositories;

public interface IPersonRepository
{
    Task<Person?> GetByIdAsync(Guid id);
    Task<IEnumerable<Person>> GetAllAsync();
    Task AddAsync(Person person);
    Task DeleteAsync(Person person);
    Task SaveChangesAsync();
}
