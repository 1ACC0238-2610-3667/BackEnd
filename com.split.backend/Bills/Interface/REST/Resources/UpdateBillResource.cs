using com.split.backend.Bills.Domain.Models.Aggregates;

namespace com.split.backend.Bills.Interface.REST.Resources;

public record UpdateBillResource(
    string? Description,
    decimal? Amount,
    string? PaymentDate
);