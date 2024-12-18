using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using DotBlogs.Models.Domains;

namespace DotBlogs.Models.BindingModels;

public class CategoryUpdateBindingModel
{
    [Required] public string? Id { get; set; }
    [Required] public string? Name { get; set; }

    public Category ToEntity()
    {
        return new()
        {
            Id = Id ?? string.Empty,
            Name = Name ?? string.Empty,
        };
    }
}