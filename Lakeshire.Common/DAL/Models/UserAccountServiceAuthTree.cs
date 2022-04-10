namespace Lakeshire.Common.DAL.Models;

public class UserAccountServiceAuthTree
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ActiveNodeId { get; set; }
    public DateTime ExpiresAt { get; set; }
    
    public UserAccount? User { get; set; }
    public UserAccountServiceAuthTreeNode? ActiveNode { get; set; }
}