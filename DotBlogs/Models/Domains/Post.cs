using System;
using System.Collections.Generic;

namespace DotBlogs.Models.Domains;

public partial class Post
{
    public string Id { get; set; } = null!;

    public string? Title { get; set; }

    public string? Content { get; set; }

    public string AuthorId { get; set; } = null!;

    public string? CategoryId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual AspNetUser Author { get; set; } = null!;

    public virtual Category? Category { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
