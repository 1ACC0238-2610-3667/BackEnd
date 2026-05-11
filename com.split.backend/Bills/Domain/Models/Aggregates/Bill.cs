using com.split.backend.Bills.Domain.Models.Commands;

namespace com.split.backend.Bills.Domain.Models.Aggregates;

public partial class Bill
{
    public string Id { get; set; }
    public string HouseholdId { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public long CreatedBy { get; set; }
    public DateTime? PaymentDate { get; set; }

    public Bill()
    {
        this.Id = String.Empty;
        this.HouseholdId = String.Empty;
        this.Description = String.Empty;
        this.Amount = Decimal.Zero;
        this.CreatedBy = 0;
        this.PaymentDate = null;
        this.CreatedDate = null;
    }

    public Bill(string householdId, string description, decimal amount,
        long createdBy, string paymentDate)
    {
        this.Id = "BG" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        this.HouseholdId = householdId;
        this.Description = description;
        this.Amount = amount;
        this.CreatedBy = createdBy;
        this.PaymentDate = DateTime.Parse(paymentDate);
        this.CreatedDate = DateTime.UtcNow;
    }

    public Bill(CreateBillCommand command)
    {
        this.Id = "BG" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        this.HouseholdId = command.HouseholdId;
        this.Description = command.Description;
        this.Amount = command.Amount;
        this.CreatedBy = command.CreatedBy;
        this.PaymentDate = DateTime.Parse(command.PaymentDate);
        this.CreatedDate = DateTime.UtcNow;
    }

    public Bill UpdateBill(UpdateBillCommand command)
    {
        if(!string.IsNullOrWhiteSpace(command.Description)) this.Description = command.Description;
        if(command.Amount != null) this.Amount = (decimal)command.Amount;
        if(!string.IsNullOrWhiteSpace(command.PaymentDate)) this.PaymentDate = DateTime.Parse(command.PaymentDate);
        
        return this;
    }


}