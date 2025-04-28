
using PersonSrv.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonSrv.Infrastructure.Data;
using PersonSrv.Domain.Entities;



namespace PersonSrv.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<PersonRepository> _logger;

    public PersonRepository(AppDbContext context, ILogger<PersonRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Person?> GetByIdAsync(Guid id)
    {
        try
        {
            return await _context.Persons.FindAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving person with ID {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        try
        {
            return await _context.Persons.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all persons");
            throw;
        }
    }

    public async Task AddAsync(Person person)
    {
        try
        {
            await _context.Persons.AddAsync(person);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding person with ID {Id}", person.Id);
            throw;
        }
    }

    public async Task DeleteAsync(Person person)
    {
        try
        {
            _context.Persons.Remove(person);
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting person with ID {Id}", person.Id);
            throw;
        }
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving changes to database");
            throw;
        }
    }
}