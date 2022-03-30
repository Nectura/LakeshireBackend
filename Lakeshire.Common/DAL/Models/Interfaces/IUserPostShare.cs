namespace Lakeshire.Common.DAL.Models.Interfaces;

public interface IUserPostShare
{
    string Id { get; set; }
    string UserId { get; set; }
    string SharedPostId { get; set; }
    string CommentPostId { get; set; }
    
    /// <summary>
    /// The user who shared the post
    /// </summary>
    UserAccount? User { get; set; }
    
    /// <summary>
    /// The post the user shared
    /// </summary>
    UserPost? SharedPost { get; set; }
    
    /// <summary>
    /// The comment post the user made by sharing the post on their timeline
    /// </summary>
    UserPost? CommentPost { get; set; }
}