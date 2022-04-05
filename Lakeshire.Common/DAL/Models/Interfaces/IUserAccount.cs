using Lakeshire.Common.Enums;

namespace Lakeshire.Common.DAL.Models.Interfaces;

public interface IUserAccount
{
    Guid Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string EmailAddress { get; set; }
    EGender Gender { get; set; }
    DateTime CreatedAt { get; set; }

    byte[] PasswordHash { get; set; }
    byte[] SaltHash { get; set; }
    
    ICollection<UserPost>? Posts { get; set; }
}