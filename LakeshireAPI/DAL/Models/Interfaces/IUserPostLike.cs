namespace LakeshireAPI.DAL.Models.Interfaces;

public interface IUserPostLike
{
    string UserId { get; set; }
    string PostId { get; set; }
    
    /// <summary>
    /// The user who liked the post
    /// </summary>
    IUserAccount User { get; set; }
    
    /// <summary>
    /// The post the user liked
    /// </summary>
    IUserPost Post { get; set; }
}