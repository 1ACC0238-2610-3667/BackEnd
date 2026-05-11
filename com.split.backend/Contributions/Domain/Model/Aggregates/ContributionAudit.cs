using System.ComponentModel.DataAnnotations.Schema;

namespace com.split.backend.Contributions.Domain.Model.Aggregates;

public partial class Contribution
{
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; } 
}