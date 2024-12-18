using DotBlogs.Models.BindingModels;
using DotBlogs.Models.Domains;
using static DotBlogs.Models.BaseResponseModel;

namespace DotBlogs.BusinessProviders;

public interface ICategoryBusinessProvider
{
    public Task<Responses<Category>> Get(CategoryListBindingModel categoryListBindingModel);
    
    public Task<Response<Category>> GetById(string id);

    public Task<Response<Category>> Create(Category category);

    public Task<Response<Category>> Update(Category category);

    public Task<Response<Category>> Delete(string categoryId);
}