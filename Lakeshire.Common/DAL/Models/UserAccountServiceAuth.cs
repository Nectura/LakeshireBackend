using Lakeshire.Common.DAL.Models.Interfaces;

namespace Lakeshire.Common.DAL.Models;

public class UserAccountServiceAuth : IUserAccountServiceAuth
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string RefreshToken { get; set; } = "";
    public string PreviousRefreshToken { get; set; } = "";
    public string Scopes { get; set; } = "";
    public DateTime AbsoluteExpirationTime { get; set; }
    
    public UserAccount? User { get; set; }
}