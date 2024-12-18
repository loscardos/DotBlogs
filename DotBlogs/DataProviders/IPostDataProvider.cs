using DotBlogs.Models.BindingModels;
using DotBlogs.Models.Domains;
using static DotBlogs.Models.BaseResponseModel;

namespace DotBlogs.DataProviders;

public interface IPostDataProvider
{
    public Task<Responses<Post>> Get(PostListBindingModel postListBindingModel);
    
    public Task<Post?> GetById(string id);

    public Task<Post?> Create(Post post);

    public Task<Post?> Update(Post post);

    public Task<Post?> Delete(string postId);
    
}