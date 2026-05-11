using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace com.split.backend.IAM.Domain.Model.Aggregates;

public partial class User : IEntityWithCreatedUpdatedDate
{
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; } 
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; } 
}