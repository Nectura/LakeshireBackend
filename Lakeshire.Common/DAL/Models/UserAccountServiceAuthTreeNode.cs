namespace Lakeshire.Common.DAL.Models;

public class UserAccountServiceAuthTreeNode
{
    public Guid Id { get; set; }
    public Guid AuthTreeId { get; set; }
    public string RefreshToken { get; set; } = "";
    public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiredAt { get; set; }
    
    public UserAccountServiceAuthTree? AuthTree { get; set; }
}