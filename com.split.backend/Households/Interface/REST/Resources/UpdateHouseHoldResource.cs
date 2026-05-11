namespace com.split.backend.Households.Interface.REST.Resources;

public class UpdateHouseHoldResource
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int MemberCount { get; set; } = 1;
    public string Currency { get; set; } = "USD";
    public DateTime? StartDate { get; set; }
}
