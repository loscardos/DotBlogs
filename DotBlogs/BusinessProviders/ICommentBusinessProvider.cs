using DotBlogs.Models.BindingModels;
using DotBlogs.Models.Domains;
using static DotBlogs.Models.BaseResponseModel;

namespace DotBlogs.BusinessProviders;

public interface ICommentBusinessProvider
{
    public Task<Responses<Comment>> Get(CommentListBindingModel commentListBindingModel);
    
    public Task<Response<Comment>> GetById(string id);

    public Task<Response<Comment>> Create(Comment comment);
}