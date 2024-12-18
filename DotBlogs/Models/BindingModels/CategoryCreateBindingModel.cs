using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using DotBlogs.Models.Domains;

namespace DotBlogs.Models.BindingModels;

public class CategoryCreateBindingModel
{
    [Required]
    [MinLength(8, ErrorMessage = "The Name must be at least 8 characters long.")]
    public string? Name { get; set; }

    public Category ToEntity()
    {
        return new()
        {
            Id = Guid.NewGuid().ToString(),
            Name = Name ?? string.Empty,
        };
    }
}