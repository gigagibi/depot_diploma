using Depot.Database;
using Depot.Models.Requisitions;

namespace Depot.Repositories.Requisitions;

public class RequisitionsRepository : GenericRepository<Requisition>, IRequisitionsRepository
{
    public RequisitionsRepository(DepotDbContext context) : base(context)
    {
    }

    public override async Task<Requisition> CreateAsync(Requisition t)
    {
        t.LastUpdateTime = DateTime.Now;
        return await base.CreateAsync(t);
    }

    public override async Task<Requisition> UpdateAsync(Requisition t)
    {
        t.LastUpdateTime = DateTime.Now;
        return await base.UpdateAsync(t);
    }
}