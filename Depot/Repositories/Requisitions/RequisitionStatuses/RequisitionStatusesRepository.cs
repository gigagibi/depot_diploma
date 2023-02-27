using Depot.Database;
using Depot.Models.Requisitions;

namespace Depot.Repositories.Requisitions.RequisitionStatuses;

public class RequisitionStatusesRepository : GenericRepository<RequisitionStatus>, IRequisitionStatusesRepository
{
    public RequisitionStatusesRepository(DepotDbContext context) : base(context)
    {
    }
}