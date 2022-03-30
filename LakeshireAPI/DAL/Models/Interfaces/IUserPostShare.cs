namespace LakeshireAPI.DAL.Models.Interfaces;

public interface IUserPostShare
{
    string UserId { get; set; }
    string SharedPostId { get; set; }
    string CommentPostId { get; set; }
    
    /// <summary>
    /// The user who shared the post
    /// </summary>
    IUserAccount User { get; set; }
    
    /// <summary>
    /// The post the user shared
    /// </summary>
    IUserPost SharedPost { get; set; }
    
    /// <summary>
    /// The comment post the user made by sharing the post on their timeline
    /// </summary>
    IUserPost CommentPost { get; set; }
}