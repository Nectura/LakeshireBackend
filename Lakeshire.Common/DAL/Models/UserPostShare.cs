using Lakeshire.Common.DAL.Models.Interfaces;

namespace Lakeshire.Common.DAL.Models;

public class UserPostShare : IUserPostShare
{
    public string Id { get; set; } = "";
    public string UserId { get; set; } = "";
    public string SharedPostId { get; set; } = "";
    public string CommentPostId { get; set; } = "";
    
    public UserAccount? User { get; set; }
    public UserPost? SharedPost { get; set; }
    public UserPost? CommentPost { get; set; }
}