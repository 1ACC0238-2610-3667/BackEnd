namespace com.split.backend.IAM.Interface.REST.Resources.UserIncome;

public record CreatedUserIncomeResource(
    string Id,
    long UserId,
    decimal Income);
