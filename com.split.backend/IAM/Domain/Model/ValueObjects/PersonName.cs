using com.split.backend.IAM.Domain.Model.Aggregates;

namespace com.split.backend.IAM.Domain.Model.ValueObjects;

public class PersonName
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public PersonName()
    {
        Id = Guid.NewGuid();
        FirstName = String.Empty;
        LastName =String.Empty;;
    }

    public PersonName(string firstName)
    {
        FirstName = firstName;
        LastName = String.Empty;
    }
    public string FullName => $"{FirstName} {LastName}";
    
    
    public int UserId { get; private set; }
    public User User { get; private set; }
}