using System.ComponentModel.DataAnnotations.Schema;

namespace com.split.backend.Households.Domain.Models.Aggregates;

public partial class IncomeAllocation
{
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
    
}