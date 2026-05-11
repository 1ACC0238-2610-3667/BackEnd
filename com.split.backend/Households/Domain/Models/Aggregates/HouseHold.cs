using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.split.backend.Households.Domain.Models.Commands;
using com.split.backend.Households.Domain.Models.ValueObjects;
using com.split.backend.IAM.Domain.Model.Aggregates;

namespace com.split.backend.Households.Domain.Models.Aggregates;

public partial class HouseHold
{
    [Column("Id")] 
    [Required]
    public string Id { get; set; }
    [Column("Name")]
    public string Name { get; set; }
    [Column("Description")]
    public string Description { get; set; }
    [Column("MemberCount")]
    public int MemberCount { get; set; }
    [Column("StartDate")]
    public DateTime? StartDate { get; set; }
    [Column("RepresentativeId")]
    [Required]
    public long RepresentativeId { get; set; }
    [Column("Currency")]
    [Required]
    public ECurrency? Currency { get; set; }

    public HouseHold()
    {
        this.Id = String.Empty;
        this.Name = String.Empty;
        this.Description = String.Empty;
        this.MemberCount = 0;
        this.StartDate = null;
        this.RepresentativeId = -1;
        this.Currency = null;
    }

    public HouseHold(string name,
        long representativeId, string currency, string description, int memberCount)
    {
        this.Id = "HH" + DateTimeOffset.UtcNow.Ticks;
        this.Name = name;
        this.Description = description;
        this.MemberCount = memberCount;
        this.StartDate = new DateTime();
        this.RepresentativeId = representativeId;
        this.Currency = Enum.Parse<ECurrency>(currency);
    }

    public HouseHold(CreateHouseholdCommand command)
    {
        Id = !string.IsNullOrWhiteSpace(command.Id)
            ? command.Id
            : "HH" + DateTimeOffset.UtcNow.Ticks;

        this.Name = command.Name;
        this.Description = command.Description ?? string.Empty;
        this.MemberCount = command.MemberCount <= 0 ? 1 : command.MemberCount;
        this.StartDate = command.StartDate ?? DateTime.UtcNow;
        this.RepresentativeId = command.RepresentativeId;

        if (!Enum.TryParse<ECurrency>(command.Currency, true, out var parsedCurrency))
            parsedCurrency = ECurrency.USD;

        this.Currency = parsedCurrency;
    }


    public HouseHold UpdateHouseHold(UpdateHouseHoldCommand command)
    {
        if (!string.IsNullOrWhiteSpace(command.Name)) this.Name = command.Name;
        this.Description = command.Description ?? string.Empty;
        if (command.MemberCount > 0) this.MemberCount = command.MemberCount;
        if (command.RepresentativeId > 0) this.RepresentativeId = command.RepresentativeId;
        if (!string.IsNullOrWhiteSpace(command.Currency) &&
            Enum.TryParse<ECurrency>(command.Currency, true, out var parsedCurrency))
        {
            this.Currency = parsedCurrency;
        }
        if (command.StartDate.HasValue) this.StartDate = command.StartDate;
        
        return this;
    }
    


}
