using Lakeshire.Common.Enums;

namespace LakeshireAPI.DAL.Models.Interfaces;

public interface IUserAccount
{
    string Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string EmailAddress { get; set; }
    EGender Gender { get; set; }
    DateTime CreatedAt { get; set; }

    byte[] PasswordHash { get; set; }
    byte[] SaltHash { get; set; }
    
    ICollection<IUserPost> Posts { get; set; }
}