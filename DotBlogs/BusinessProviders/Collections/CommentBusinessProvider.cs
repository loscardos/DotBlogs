using DotBlogs.DataProviders;
using DotBlogs.Helpers;
using DotBlogs.Models.BindingModels;
using DotBlogs.Models.Domains;
using static DotBlogs.Models.BaseResponseModel;

namespace DotBlogs.BusinessProviders.Collections;

public class CommentBusinessProvider : ICommentBusinessProvider
{
    private readonly ILogger<CommentBusinessProvider> _logger;
    private readonly ICommentDataProvider _dataProvider;

    public CommentBusinessProvider(
        ILogger<CommentBusinessProvider> logger,
        ICommentDataProvider dataProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
    }

    public async Task<Responses<Comment>> Get(CommentListBindingModel commentListBindingModel)
    {
        Responses<Comment> responses = await _dataProvider.Get(commentListBindingModel);

        if (responses.Data == null || !responses.Data.Any())
            return new Responses<Comment>
            {
                StatusCode = StatusCodes.Status200OK,
                Message = ApplicationConstant.NoContentMessage
            };
        
        return responses;
    }

    public async Task<Response<Comment>> GetById(string id)
    {
        Comment? comment = await _dataProvider.GetById(id);

        if (comment == null)
            return new()
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = ApplicationConstant.DataNotFound
            };

        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = comment
        };
    }

    public async Task<Response<Comment>> Create(Comment comment)
    {
        Comment? commentCreate = await _dataProvider.Create(comment);

        if (commentCreate == null)
            return new()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = ApplicationConstant.NotOkMessage
            };

        return new()
        {
            StatusCode = StatusCodes.Status200OK,
            Data = comment
        };
    }
}