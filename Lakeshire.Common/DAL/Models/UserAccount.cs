using Lakeshire.Common.DAL.Models.Interfaces;
using Lakeshire.Common.Enums;

namespace Lakeshire.Common.DAL.Models;

public class UserAccount : IUserAccount
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string EmailAddress { get; set; } = "";
    public EGender Gender { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    public byte[] SaltHash { get; set; } = Array.Empty<byte>();

    public ICollection<UserPost>? Posts { get; set; }
    public ICollection<UserAccountServiceAuth>? ServiceAuths { get; set; }
}