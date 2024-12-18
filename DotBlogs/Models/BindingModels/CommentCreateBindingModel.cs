using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using DotBlogs.Models.Domains;

namespace DotBlogs.Models.BindingModels;

public class CommentCreateBindingModel
{
    [Required]
    public string PostId { get; set; } = null!;

    [Required]
    public string UserId { get; set; } = null!;

    public string? ParentId { get; set; }

    [Required]
    public string? Content { get; set; }

    public Comment ToEntity()
    {
        return new()
        {
            Id = Guid.NewGuid().ToString(),
            PostId = PostId,
            UserId = UserId,
            ParentId = ParentId,
            Content = Content,
        };
    }
}