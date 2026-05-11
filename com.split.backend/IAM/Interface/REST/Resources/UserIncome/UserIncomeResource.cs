namespace com.split.backend.IAM.Interface.REST.Resources.UserIncome;

public record UserIncomeResource(
    string Id,
    long UserId,
    decimal Income,
    string CreatedDate,
    string UpdatedDate);
