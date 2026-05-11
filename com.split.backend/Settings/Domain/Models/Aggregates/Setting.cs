using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.split.backend.Settings.Domain.Models.Commands;

namespace com.split.backend.Settings.Domain.Models.Aggregates;

public partial class Setting
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id")]
    public long Id { get; private set; }

    [Required]
    [Column("UserId")]
    public int UserId { get; private set; }

    [Required]
    [MaxLength(10)]
    [Column("Language")]
    public string Language { get; private set; } = string.Empty;

    [Column("DarkMode")]
    public bool DarkMode { get; private set; }

    [Column("NotificationEnabled")]
    public bool NotificationEnabled { get; private set; }

    [Column("CreatedAt")]
    public DateTime CreatedAt { get; private set; }

    [Column("UpdatedAt")]
    public DateTime UpdatedAt { get; private set; }

    public Setting()
    {
    }

    public Setting(CreateSettingCommand command)
    {
        UserId = checked((int)command.UserId);
        Language = command.Language;
        DarkMode = command.DarkMode;
        NotificationEnabled = command.NotificationEnabled;
        var now = DateTime.UtcNow;
        CreatedAt = now;
        UpdatedAt = now;
    }

    public Setting Update(UpdateSettingCommand command)
    {
        Language = command.Language;
        DarkMode = command.DarkMode;
        NotificationEnabled = command.NotificationEnabled;
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    public void RefreshUpdatedAt()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
