using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using DotBlogs.Models.Domains;

namespace DotBlogs.Models.BindingModels;

public class PostUpdateBindingModel
{
    [Required] public string? Id { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "The Title must be at least 8 characters long.")]
    public string? Title { get; set; }

    [Required]
    public string? Content { get; set; }

    [Required]
    public string AuthorId { get; set; } = null!;

    public string? CategoryId { get; set; }

    public Post ToEntity()
    {
        return new()
        {
            Id = Id ?? string.Empty,
            Title = Title,
            Content = Content,
            AuthorId = AuthorId,
            CategoryId = CategoryId
        };
    }
}