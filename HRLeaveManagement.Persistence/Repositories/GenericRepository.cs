using System.Collections.Immutable;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain.Common;
using HRLeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly HRDatabaseContext _context;

    public GenericRepository(HRDatabaseContext context)
    {
        _context = context;
    }

    public async Task AddAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public Task DeleteAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
        }
        _context.Remove(entity);
        return _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id) != null;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e=>e.Id == id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity with ID {id} not found");
        }
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
        }
       // _context.Update(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}