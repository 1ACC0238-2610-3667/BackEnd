using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace com.split.backend.IAM.Domain.Model.Aggregates;

public partial class UserIncome : IEntityWithCreatedUpdatedDate
{
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; } 
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
}