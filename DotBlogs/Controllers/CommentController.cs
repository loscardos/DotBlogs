using DotBlogs.BusinessProviders;
using DotBlogs.Helpers;
using DotBlogs.Models.BindingModels;
using DotBlogs.Models.Domains;
using Microsoft.AspNetCore.Mvc;
using static DotBlogs.Models.BaseResponseModel;

namespace DotBlogs.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
[ValidateModel]
public class CommentController : ControllerBase
{
    private readonly ILogger<CommentController> _logger;
    private readonly ICommentBusinessProvider _businessProvider;

    public CommentController(
        ILogger<CommentController> logger,
        ICommentBusinessProvider businessProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _businessProvider = businessProvider ?? throw new ArgumentNullException(nameof(businessProvider));
    }

    [HttpGet]
    [ProducesResponseType(typeof(Responses<Comment>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Responses), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Responses), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Responses), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromQuery] CommentListBindingModel salesOrderParamModel)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();

            Responses<Comment> responses = await _businessProvider.Get(salesOrderParamModel);

            return StatusCode(responses.StatusCode, responses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new Responses
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                });
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(Response<Comment>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(string id)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();

            Response<Comment> response = await _businessProvider.GetById(id);

            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new Response<Comment>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                });
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(Response<Comment>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CommentCreateBindingModel commentCreateBindingModel)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();

            Response<Comment> response = await _businessProvider.Create(commentCreateBindingModel.ToEntity());

            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new Response<Comment>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                });
        }
    }
}