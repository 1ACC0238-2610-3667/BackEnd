using com.split.backend.IAM.Domain.Model.ValueObjects;

namespace com.split.backend.IAM.Domain.Model.Aggregates;

public partial class User
{
    public EPlan? Plan { get; set; }
    public Uri? Photo { get; set; }
    public DateTime? ProfileLockedUntil { get; set; }
    public bool? IsNewUser { get; set; }
    
}