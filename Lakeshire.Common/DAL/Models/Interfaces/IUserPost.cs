namespace Lakeshire.Common.DAL.Models.Interfaces;

public interface IUserPost
{
    Guid Id { get; set; }
    Guid UserId { get; set; }
    string Content { get; set; }
    int Comments { get; set; }
    int Shares { get; set; }
    int Likes { get; set; } 
    DateTime CreatedAt { get; set; }
    
    UserAccount? User { get; set; }
    //ICollection<UserPostComment>? PostComments { get; set; }
    //ICollection<UserPostShare>? PostShares { get; set; }
    //ICollection<UserPostLike>? PostLikes { get; set; }
}