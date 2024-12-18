using DotBlogs.Models.BindingModels;
using DotBlogs.Models.Domains;
using static DotBlogs.Models.BaseResponseModel;

namespace DotBlogs.DataProviders;

public interface ICommentDataProvider
{
    public Task<Responses<Comment>> Get(CommentListBindingModel commentListBindingModel);
    
    public Task<Comment?> GetById(string id);

    public Task<Comment?> Create(Comment comment);

}