using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using com.split.backend.Households.Domain.Models.ValueObjects;
using com.split.backend.IAM.Domain.Model.Commands;
using com.split.backend.IAM.Domain.Model.ValueObjects;

namespace com.split.backend.IAM.Domain.Model.Aggregates;

public partial class User
{
    [Column("Id")]
    public int Id {  get; set; }
    public EmailAddress Email { get; set; } = new EmailAddress();
    public PersonName PersonName { get; set; } = new PersonName();
    [Column("Password")]
    public string Password { get; set; } 
    [Column("Role")]
    public Role Role { get; set; } = new Role();
    [Column("Status")]
    public bool? Status { get; set; }
    [Column("HouseholdId")]
    public string HouseholdId { get; set; }

    
    public User()
    {
        this.Password = String.Empty;
        this.Status = false;
        this.HouseholdId = String.Empty;
    }

    public User(string email, string name, string password, 
        string role,string status, string householdId, 
        int plan, string photo, string profileLockedUntil, bool isNewUser)
    {
        this.Role = Enum.Parse<Role>(role);
        this.Password = password;
        this.Email = new EmailAddress(email);
        this.PersonName = new PersonName(name);
        this.Status = string.Equals(status, "active", StringComparison.OrdinalIgnoreCase); 
        this.HouseholdId = householdId;
        this.Plan = (EPlan)plan;
        this.Photo = new Uri(photo);
        this.ProfileLockedUntil = DateTime.Parse(profileLockedUntil);
        this.IsNewUser = isNewUser;
    }

    public User(SignUpCommand command, string hashedPassword)
    {
        this.Role = Enum.Parse<Role>(command.Role);
        this.Password = hashedPassword;
        this.Email = new EmailAddress(command.EmailAddress);
        this.PersonName = new PersonName(command.Name);
        this.HouseholdId = string.IsNullOrWhiteSpace(command.HouseholdId) ? "HH" + DateTime.Now.Ticks : command.HouseholdId;
        this.Status = true;
        this.Plan = Enum.IsDefined(typeof(EPlan), command.Plan) ? (EPlan)command.Plan : EPlan.Free;
        this.ProfileLockedUntil = null;
        this.IsNewUser = true;
    }

    public User UpdateUsername(string username)
    {
        this.PersonName = new PersonName(username);
        return this;
    }

    public User UpdatePassword(string password)
    {
        this.Password = password;
        return this;
    }
    
}
