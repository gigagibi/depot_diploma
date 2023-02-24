using System.ComponentModel.DataAnnotations.Schema;

namespace Depot.Models;

public interface IGenericModel
{
    int Id { get; set; }
    bool Archived { get; set; }
}