namespace LakeshireAPI.DAL.Models.Interfaces;

public interface IUserPostComment
{
    string UserId { get; set; }
    string PostId { get; set; }
    string CommentPostId { get; set; }
    
    /// <summary>
    /// The user who made the comment
    /// </summary>
    IUserAccount User { get; set; }
    
    /// <summary>
    /// The post the user commented on
    /// </summary>
    IUserPost Post { get; set; }
    
    /// <summary>
    /// The comment post the user made by commenting on the original post
    /// </summary>
    IUserPost CommentPost { get; set; }
}