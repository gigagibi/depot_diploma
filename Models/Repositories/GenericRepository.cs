using Depot.API;
using Depot.Database;
using Depot.Exceptions;
using Depot.Models;
using Microsoft.EntityFrameworkCore;

namespace Depot.Repositories;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : class, IGenericModel
{
    protected readonly DepotDbContext _context;
    protected readonly DbSet<T> _dbSet;

    protected GenericRepository(DepotDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T> CreateAsync(T t)
    {
        if (t is null)
        {
            throw new ArgumentNullException();
        }
        var entry = await _dbSet.AddAsync(t);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public virtual async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id) ?? throw new ModelNotFoundException();
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task ArchiveAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id) ?? throw new ModelNotFoundException();
        entity.Archived = true;
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(PaginationParameters? paginationParameters = null, bool returnArchived = false)
    {
        if (paginationParameters is not null)
        {
            return await _dbSet
                .Where(m => m.Archived == returnArchived && m.Archived == false)
                .OrderBy(on => paginationParameters.SortBy)
                .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
                .Take(paginationParameters.PageSize)
                .ToListAsync();            
        }
        return await _dbSet
            .Where(m => m.Archived == returnArchived && m.Archived == false)
            .ToListAsync();
    }

    public virtual async Task<T> GetAsync(int id, bool returnArchived = false)
    {
            
        var model = await _dbSet.Where(m => m.Id == id && (m.Archived == returnArchived || m.Archived == false)).FirstOrDefaultAsync();
        if (model is null)
        {
            throw new ModelNotFoundException();
        }
        return model;
    }

    public virtual IEnumerable<T> GetAllByPredicate(Func<T, bool> predicate, PaginationParameters? paginationParameters = null, bool returnArchived = false)
    {
        List<T> models;
        if (paginationParameters is not null)
        {
            models = _dbSet.Where(predicate)
                .Where(m => m.Archived == returnArchived && m.Archived == false)
                .OrderBy(on => paginationParameters.SortBy)
                .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
                .Take(paginationParameters.PageSize)
                .ToList();            
        }
        else
        {
            models = _dbSet.Where(predicate)
                .Where(m => m.Archived == returnArchived && m.Archived == false)
                .ToList();
        }
        if(models is null || !models.Any())
        {
            throw new ModelNotFoundException();
        }
        return models;
    }

    public virtual async Task<T> UpdateAsync(T t)
    {
        var entry = _dbSet.Update(t);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }
}