using com.split.backend.Bills.Domain.Models.Aggregates;

namespace com.split.backend.Bills.Domain.Models.Commands;

public record UpdateBillCommand(
  string Id,
  string? Description,
  decimal? Amount,
  string? PaymentDate
);