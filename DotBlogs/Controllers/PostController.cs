using DotBlogs.BusinessProviders;
using DotBlogs.Helpers;
using DotBlogs.Models.BindingModels;
using DotBlogs.Models.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static DotBlogs.Models.BaseResponseModel;

namespace DotBlogs.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
[ValidateModel]
public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;
    private readonly IPostBusinessProvider _businessProvider;
    private readonly IMemoryCache _cache;

    public PostController(
        ILogger<PostController> logger,
        IPostBusinessProvider businessProvider,
        IMemoryCache cache)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _businessProvider = businessProvider ?? throw new ArgumentNullException(nameof(businessProvider));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    [HttpGet]
    [ProducesResponseType(typeof(Responses<Post>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Responses), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Responses), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Responses), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromQuery] PostListBindingModel salesOrderParamModel)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();

            Responses<Post> responses = await _businessProvider.Get(salesOrderParamModel);

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
    [ProducesResponseType(typeof(Response<Post>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(string id)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();

            var cacheKey = $"Post_{id}";

            if (!_cache.TryGetValue(cacheKey, out Response<Post> cachedResponse))
            {
                cachedResponse = await _businessProvider.GetById(id);

                if (cachedResponse.StatusCode == StatusCodes.Status200OK)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                    _cache.Set(cacheKey, cachedResponse, cacheEntryOptions);
                }
            }

            return StatusCode(cachedResponse.StatusCode, cachedResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new Response<Post>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                });
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(Response<Post>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] PostCreateBindingModel postCreateBindingModel)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();

            Response<Post> response = await _businessProvider.Create(postCreateBindingModel.ToEntity());

            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new Response<Post>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                });
        }
    }

    [HttpPatch]
    [ProducesResponseType(typeof(Response<Post>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] PostUpdateBindingModel postUpdateBindingModel)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();

            Response<Post> response = await _businessProvider.Update(postUpdateBindingModel.ToEntity());

            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new Response<Post>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                });
        }
    }

    [HttpDelete]
    [ProducesResponseType(typeof(Response<Post>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();

            Response<Post> response = await _businessProvider.Delete(id);

            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new Response<Post>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                });
        }
    }
}