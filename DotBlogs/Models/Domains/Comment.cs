using System;
using System.Collections.Generic;

namespace DotBlogs.Models.Domains;

public partial class Comment
{
    public string Id { get; set; } = null!;

    public string PostId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string? ParentId { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Comment> InverseParent { get; set; } = new List<Comment>();

    public virtual Comment? Parent { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}
