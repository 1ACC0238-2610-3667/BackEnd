using com.split.backend.Bills.Domain.Models.Aggregates;
using com.split.backend.Bills.Domain.Models.Commands;

namespace com.split.backend.Bills.Domain.Services;

public interface IBillCommandService
{
    Task<Bill?> Handle(CreateBillCommand command);
    Task<Bill?> Handle(UpdateBillCommand command);
    Task<bool> Handle(DeleteBillCommand command);
}