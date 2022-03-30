using Lakeshire.Common.DAL.Models.Interfaces;

namespace Lakeshire.Common.DAL.Models;

public class UserPostLike : IUserPostLike
{
    public string UserId { get; set; }= "";
    public string PostId { get; set; }= "";
    
    public UserAccount? User { get; set; }
    public UserPost? Post { get; set; }
}