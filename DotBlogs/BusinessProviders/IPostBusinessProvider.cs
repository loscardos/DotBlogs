using DotBlogs.Models.BindingModels;
using DotBlogs.Models.Domains;
using static DotBlogs.Models.BaseResponseModel;

namespace DotBlogs.BusinessProviders;

public interface IPostBusinessProvider
{
    public Task<Responses<Post>> Get(PostListBindingModel postListBindingModel);
    
    public Task<Response<Post>> GetById(string id);

    public Task<Response<Post>> Create(Post post);

    public Task<Response<Post>> Update(Post post);

    public Task<Response<Post>> Delete(string postId);
}