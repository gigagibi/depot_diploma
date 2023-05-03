using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Depot.Models.Users;

public class Role : IdentityRole<int>, IGenericModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public override int Id { get => base.Id; set => base.Id = value; }

    public bool Archived { get; set; }
}