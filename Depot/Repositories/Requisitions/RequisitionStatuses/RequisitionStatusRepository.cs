using Depot.Database;
using Depot.Models.Requisitions;

namespace Depot.Repositories.Requisitions.RequisitionStatuses;

public class RequisitionStatusRepository : GenericRepository<RequisitionStatus>, IRequisitionStatusesRepository
{
    public RequisitionStatusRepository(DepotDbContext context) : base(context)
    {
    }
}