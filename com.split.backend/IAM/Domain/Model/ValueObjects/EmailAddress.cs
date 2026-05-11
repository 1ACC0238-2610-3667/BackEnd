using com.split.backend.IAM.Domain.Model.Aggregates;

namespace com.split.backend.IAM.Domain.Model.ValueObjects;

public class EmailAddress
{
    public string Id { get; set; }
    public string Address { get; set; }

    public EmailAddress()
    {
        Id = Guid.NewGuid().ToString();
        Address = String.Empty;
    }
    public EmailAddress(string address)
    {
        Id =  Guid.NewGuid().ToString();
        Address = Normalize(address);
    }

    public static string Normalize(string email)
    {
        return (email ?? string.Empty).Trim().ToLowerInvariant();
    }
    
    public void Change(EmailAddress email)
    {
        if(email.Address.Length >0)
            this.Address = Normalize(email.Address);
        else
            throw new ArgumentException("Email address cannot be empty");
    }
    
    
    public int UserId { get; private set; }
    public User User { get; private set; }
    
}