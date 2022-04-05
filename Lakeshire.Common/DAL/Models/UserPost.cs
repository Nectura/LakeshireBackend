using Lakeshire.Common.DAL.Models.Interfaces;

namespace Lakeshire.Common.DAL.Models;

public class UserPost : IUserPost
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; } = "";
    public int Comments { get; set; }
    public int Shares { get; set; }
    public int Likes { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public UserAccount? User { get; set; }
    //public ICollection<UserPostComment>? PostComments { get; set; }
    //public ICollection<UserPostShare>? PostShares { get; set; }
    //public ICollection<UserPostLike>? PostLikes { get; set; }
}