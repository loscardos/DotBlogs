using DotBlogs.Models.BindingModels;
using DotBlogs.Models.Domains;
using static DotBlogs.Models.BaseResponseModel;

namespace DotBlogs.DataProviders;

public interface ICategoryDataProvider
{
    public Task<Responses<Category>> Get(CategoryListBindingModel categoryListBindingModel);
    
    public Task<Category?> GetById(string id);

    public Task<Category?> Create(Category category);

    public Task<Category?> Update(Category category);

    public Task<Category?> Delete(string categoryId);
}