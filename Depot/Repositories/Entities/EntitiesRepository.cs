using Depot.API;
using Depot.Database;
using Depot.Exceptions;
using Depot.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Depot.Repositories.Entities;

public class EntitiesRepository : GenericRepository<Entity>, IEntitiesRepository
{
    public EntitiesRepository(DepotDbContext context) : base(context)
    {
    }

    public async Task<Entity> GetByInvNumberAsync(int invNumber, bool returnArchived = false)
    {
        var entity = await _context.Entities.FirstOrDefaultAsync(x => x.InvNumber == invNumber && (x.Archived == returnArchived || x.Archived == false));
        if (entity is null)
        {
            throw new ModelNotFoundException("Entity with given invNumber not found");
        }
        return entity;
    }

    public async Task<int> GenerateInvNumberAsync()
    {
        var lastEntity = await _context.Entities.OrderByDescending(e => e.InvNumber).FirstOrDefaultAsync();
        return lastEntity == null ? 1 : lastEntity.InvNumber + 1;
    }

    public async Task<bool> CheckIfInvNumberExistsAsync(int invNumber)
    {
        return await _context.Entities.AnyAsync(e => e.InvNumber == invNumber);
    }

    public async Task<IEnumerable<Entity>> GetPartsAsync(int id, PaginationParameters? paginationParameters = null)
    {
        var father = await _context.Entities.FindAsync(id) ?? throw new ModelNotFoundException("Entity with given id not found");
        if (!father.FatherOf) return new List<Entity>();
        if (paginationParameters is not null)
        {
            return await _context.Entities.Where(e => e.PartOfField == father.Id)
                .OrderBy(on => paginationParameters.SortBy)
                .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
                .Take(paginationParameters.PageSize)
                .ToListAsync();                    
        }
        return await _context.Entities.Where(e => e.PartOfField == father.Id)
            .ToListAsync();
    }

    public async Task<IEnumerable<Entity>> GetPartsByInvNumberAsync(int invNumber, PaginationParameters? paginationParameters = null)
    {
        var father = await _context.Entities.Where(x => x.InvNumber == invNumber).FirstOrDefaultAsync() ?? throw new ModelNotFoundException("Entity with given invNumber not found");
        if (!father.FatherOf) return new List<Entity>();
        if (paginationParameters is not null)
        {
            return await _context.Entities.Where(e => e.PartOfField == father.Id)
                .OrderBy(on => paginationParameters.SortBy)
                .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
                .Take(paginationParameters.PageSize)
                .ToListAsync();                
        }
        return await _context.Entities.Where(e => e.PartOfField == father.Id)
            .ToListAsync();
    }

    public async Task<Entity> GetFatherAsync(int id)
    {
        var entity = await _context.Entities.FindAsync(id) ?? throw new ModelNotFoundException("Entity with given id not found");
        if (entity.PartOf)
        {
            return await _context.Entities.FindAsync(entity.PartOfField) ?? throw new ModelNotFoundException("Father of part with given id not found");
        }
        throw new ModelNotFoundException("Entity with given id is not a part");
    }
    
    public async Task<Entity> GetFatherByInvNumberAsync(int invNumber)
    {
        var entity = await _context.Entities.Where(x => x.InvNumber == invNumber).FirstOrDefaultAsync() ?? throw new ModelNotFoundException("Entity with given invNumber not found");;
        if (entity.PartOf)
        {
            return await _context.Entities.FindAsync(entity.PartOfField) ?? throw new ModelNotFoundException("Father of part with given invNumber not found");
        }
        throw new ModelNotFoundException("Entity with given invNumber is not a part");
    }

    public async Task DeleteByInvNumberAsync(int invNumber)
    {
        var entity = await _dbSet.Where(x => x.InvNumber == invNumber).FirstOrDefaultAsync() ?? throw new ModelNotFoundException("Entity with given invNumber not found");
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task ArchiveByInvNumberAsync(int invNumber)
    {
        var entity = await _dbSet.Where(x => x.InvNumber == invNumber).FirstOrDefaultAsync() ?? throw new ModelNotFoundException("Entity with given invNumber not found");
        entity.Archived = true;
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
}