namespace Lakeshire.Common.DAL.Models.Interfaces;

public interface IUserPostComment
{
    string PostId { get; set; }
    string CommentPostId { get; set; }
    
    /// <summary>
    /// The post the user commented on
    /// </summary>
    UserPost? Post { get; set; }
    
    /// <summary>
    /// The comment post the user made by commenting on the original post
    /// </summary>
    UserPost? CommentPost { get; set; }
}