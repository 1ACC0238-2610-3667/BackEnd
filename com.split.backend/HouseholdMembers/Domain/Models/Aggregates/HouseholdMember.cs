using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace com.split.backend.HouseholdMembers.Domain.Models.Aggregates;

public class HouseholdMember
{
    [Key]
    [Column("Id")]
    [Required]
    public string Id { get; set; } = string.Empty;

    [Column("HouseholdId")]
    [Required]
    public string HouseholdId { get; set; } = string.Empty;

    [Column("UserId")]
    [Required]
    public int UserId { get; set; }

    [Column("IsRepresentative")]
    [Required]
    public bool IsRepresentative { get; set; }

    [Column("Income")]
    public decimal Income { get; set; } = 0m;

    [Column("JoinedAt")]
    [Required]
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    [Column("CreatedAt")]
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("UpdatedAt")]
    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public HouseholdMember() {}

    public HouseholdMember(string householdId, int userId, bool isRepresentative, decimal income = 0m)
    {
        Id = $"HM-{DateTime.UtcNow.Ticks}";
        HouseholdId = householdId;
        UserId = userId;
        IsRepresentative = isRepresentative;
        Income = income;
        JoinedAt = DateTime.UtcNow;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public HouseholdMember PromoteToRepresentative()
    {
        IsRepresentative = true;
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    public HouseholdMember DemoteRepresentative()
    {
        IsRepresentative = false;
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    public HouseholdMember UpdateIncome(decimal? income, bool? isRepresentative)
    {
        if (income.HasValue) Income = income.Value;
        if (isRepresentative.HasValue) IsRepresentative = isRepresentative.Value;
        UpdatedAt = DateTime.UtcNow;
        return this;
    }
}
