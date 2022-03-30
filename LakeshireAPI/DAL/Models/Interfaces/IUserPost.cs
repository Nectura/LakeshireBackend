namespace LakeshireAPI.DAL.Models.Interfaces;

public interface IUserPost
{
    string Id { get; set; }
    string UserId { get; set; }
    string Content { get; set; }
    int Comments { get; set; }
    int Shares { get; set; }
    int Likes { get; set; } 
    DateTime CreatedAt { get; set; }
    
    IUserAccount Poster { get; set; }
    ICollection<IUserPostComment> PostComments { get; set; }
    ICollection<IUserPostShare> PostShares { get; set; }
    ICollection<IUserPostLike> PostLikes { get; set; }
}