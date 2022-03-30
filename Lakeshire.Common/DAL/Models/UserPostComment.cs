using System.ComponentModel.DataAnnotations.Schema;
using Lakeshire.Common.DAL.Models.Interfaces;

namespace Lakeshire.Common.DAL.Models;

public class UserPostComment : IUserPostComment
{
    public string PostId { get; set; } = "";
    public string CommentPostId { get; set; } = "";
    
    [ForeignKey(nameof(PostId))]
    public UserPost? Post { get; set; }

    [ForeignKey(nameof(CommentPostId))]
    public UserPost? CommentPost { get; set; }
}