using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using com.split.backend.MemberContributions.Domain.Model.Commands;
using com.split.backend.MemberContributions.Domain.Model.ValueObjects;

namespace com.split.backend.MemberContributions.Domain.Model.Aggregates;

public partial class MemberContribution
{
    [NotNull]
    public string Id {get; init;}
    public string ContributionId {get; set;}
    public string MemberId {get; set;}
    private decimal ContributionSoFar {get; set;}
    public decimal Amount {get; set;}
    public EStatus Status {get; set;}
    public DateTime PayedAt {get; set;}

    public MemberContribution()
    {
        Id = Guid.NewGuid().ToString();
        ContributionId = String.Empty;
        MemberId = String.Empty;
        Amount = 0;
        Status = EStatus.Pending;
        PayedAt = DateTime.Parse("0001-01-01 00:00:00");
   }

    public MemberContribution(string contributionId,
        string memberId, decimal amount, int status)
    {
        Id ="MC"+ "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        ContributionId = contributionId;
        MemberId = memberId;
        ContributionSoFar = 0;
        Amount = amount;
        Status= (EStatus)status;
        PayedAt = DateTime.Parse("0001-01-01 00:00:00");
 
    }

    public MemberContribution(CreateMemberContributionCommand command)
    {
        Id ="MC"+ "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        ContributionId = command.ContributionId;
        MemberId = command.MemberId;
        ContributionSoFar = 0;
        Amount = command.Amount;
        Status = EStatus.Pending;
        PayedAt = DateTime.Parse("0001-01-01 00:00:00");
    }

    public MemberContribution UpdateAmount(UpdateMemberContributionAmountCommand command)
    {
        if(ContributionSoFar + command.Amount > command.Amount)
            throw new ArgumentException("The amount contributed cannot be greater than the total amount to be contributed");
        
        ContributionSoFar += command.Amount;
        if (ContributionSoFar == command.Amount)
        {
            PayedAt = DateTime.Now;
            Status = EStatus.Done;
        }
        return this;
    }
}