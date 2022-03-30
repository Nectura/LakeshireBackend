using Lakeshire.Common.Enums;
using LakeshireAPI.DAL.Models.Interfaces;

namespace LakeshireAPI.DAL.Models;

public class UserAccount : IUserAccount
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public EGender Gender { get; set; }
    public DateTime CreatedAt { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] SaltHash { get; set; }
    public ICollection<IUserPost> Posts { get; set; }
}