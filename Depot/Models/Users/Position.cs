using Depot.Models.Departments;
using System.ComponentModel.DataAnnotations.Schema;

namespace Depot.Models.Users
{
    public class Position : IGenericModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        public bool Archived { get; set; }

        public string Name { get; set; }
    }
}
