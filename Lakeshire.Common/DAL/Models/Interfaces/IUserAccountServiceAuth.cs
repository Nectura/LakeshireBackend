namespace Lakeshire.Common.DAL.Models.Interfaces;

public interface IUserAccountServiceAuth
{
    Guid UserId { get; set; }
    string RefreshToken { get; set; }
    string Scopes { get; set; }
    DateTime AbsoluteExpirationTime { get; set; }
    
    UserAccount? User { get; set; }
}