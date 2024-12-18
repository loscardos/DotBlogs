using System;
using System.Collections.Generic;

namespace DotBlogs.Models.Domains;

public partial class Category
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
