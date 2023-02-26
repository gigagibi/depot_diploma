namespace Depot.Models.Requisitions;

public class RequisitionStatusName
{
    public const string Created = "Created";
    public const string Approved = "Approved";
    public const string Declined = "Declined";

    public static IEnumerable<string> AllRoles
    {
        get
        {
            yield return Created;
            yield return Approved;
            yield return Declined;
        }
    }
}