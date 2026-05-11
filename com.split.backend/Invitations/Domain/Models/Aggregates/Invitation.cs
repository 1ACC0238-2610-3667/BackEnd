using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace com.split.backend.Invitations.Domain.Models.Aggregates;

public enum InvitationStatus
{
    Pending = 0,
    Accepted = 1,
    Expired = 2,
    Revoked = 3
}

public class Invitation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string HouseholdId { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public InvitationStatus Status { get; set; } = InvitationStatus.Pending;

    [Required]
    [MaxLength(64)]
    public string Token { get; set; } = Guid.NewGuid().ToString("N");

    [Required]
    public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddDays(7);

    [Required]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedDate { get; set; }
}
