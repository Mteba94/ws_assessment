using assessment.Application.Interfaces.Persistence;
using assessment.Domain.Entities;
using assessment.Infraestructure.Persistence.Context;
using assessment.Utilities.Static;
using Microsoft.EntityFrameworkCore;

namespace assessment.Infraestructure.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _entity;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _entity = _context.Set<T>();
        //_httpContextAccessor = httpContextAccessor.HttpContext;
    }

    public IQueryable<T> GetAllQueryable()
    {
        var query = _entity.AsQueryable();
        return query;
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var getAll = await _entity
                .Where(x => x.Status.Equals((int)TipoEstado.Activo))
                .AsNoTracking()
                .ToListAsync();

        return getAll;
    }
    public async Task<T> GetByIdAsync(int id)
    {
        var getById = await _entity.SingleOrDefaultAsync(x => x.Id.Equals(id) && x.Status.Equals((int)TipoEstado.Activo));

        return getById!;
    }

    public async Task CreateAsync(T entity)
    {
        entity.Status = (int)TipoEstado.Activo;

        await _context.AddAsync(entity);
    }
    public void Update(T entity)
    {
        _context.Update(entity);
    }

    public async Task DeleteAsync(int id)
    {
        T entity = await GetByIdAsync(id);

        entity.Status = (int)TipoEstado.Inactivo;

        _context.Update(entity);
    }
}
