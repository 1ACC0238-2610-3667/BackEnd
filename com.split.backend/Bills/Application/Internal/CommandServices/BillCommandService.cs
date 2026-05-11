using com.split.backend.Bills.Domain.Models.Aggregates;
using com.split.backend.Bills.Domain.Models.Commands;
using com.split.backend.Bills.Domain.Models.Events;
using com.split.backend.Bills.Domain.Repositories;
using com.split.backend.Bills.Domain.Services;
using com.split.backend.Shared.Domain.Repositories;
using Cortex.Mediator;

namespace com.split.backend.Bills.Application.Internal.CommandServices;

public class BillCommandService(IBillRepository billRepository, IUnitOfWork unitOfWork, IMediator domainEventPublisher) : IBillCommandService
{
    public async Task<Bill?> Handle(CreateBillCommand command)
    {
        var bill = new Bill(command);
        
        if(billRepository.ExistsById(bill.Id)) throw new Exception("Bill already exists");
        
        await billRepository.AddAsync(bill);
        await unitOfWork.CompleteAsync();
        
        //Publish the domain event after the bill is created
        await domainEventPublisher.PublishAsync(new BillCreatedEvent(bill.Description));
        
        //Return created bill
        return bill;
    }

    public async Task<Bill?> Handle(UpdateBillCommand command)
    {
        var bill = await billRepository.FindByStringIdAsync(command.Id);
        if(bill == null) throw new Exception("Bill not found");

        bill.UpdateBill(command);
        
        billRepository.Update(bill);

        await unitOfWork.CompleteAsync();
        
        return bill;
    }

    public async Task<bool> Handle(DeleteBillCommand command)
    {
        var bill = await billRepository.FindByStringIdAsync(command.Id);
        
        if(bill == null) return false;
        
        billRepository.Remove(bill);
        await unitOfWork.CompleteAsync();
        
        return true;
    }
}