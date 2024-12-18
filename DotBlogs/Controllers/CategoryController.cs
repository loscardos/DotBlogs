using DotBlogs.BusinessProviders;
using DotBlogs.Helpers;
using DotBlogs.Models.BindingModels;
using DotBlogs.Models.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static DotBlogs.Models.BaseResponseModel;

namespace DotBlogs.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
[ValidateModel]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ICategoryBusinessProvider _businessProvider;

    public CategoryController(
        ILogger<CategoryController> logger,
        ICategoryBusinessProvider businessProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _businessProvider = businessProvider ?? throw new ArgumentNullException(nameof(businessProvider));
    }

    [HttpGet]
    [ProducesResponseType(typeof(Responses<Category>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Responses), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Responses), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Responses), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromQuery] CategoryListBindingModel salesOrderParamModel)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();

            Responses<Category> responses = await _businessProvider.Get(salesOrderParamModel);

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
    [ProducesResponseType(typeof(Response<Category>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(string id)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();

            Response<Category> response = await _businessProvider.GetById(id);

            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new Response<Category>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                });
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(Response<Category>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CategoryCreateBindingModel categoryCreateBindingModel)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();

            Response<Category> response = await _businessProvider.Create(categoryCreateBindingModel.ToEntity());

            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new Response<Category>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                });
        }
    }

    [HttpPatch]
    [ProducesResponseType(typeof(Response<Category>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] CategoryUpdateBindingModel categoryUpdateBindingModel)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();

            Response<Category> response = await _businessProvider.Update(categoryUpdateBindingModel.ToEntity());

            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new Response<Category>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                });
        }
    }

    [HttpDelete]
    [ProducesResponseType(typeof(Response<Category>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();

            Response<Category> response = await _businessProvider.Delete(id);

            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new Response<Category>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                });
        }
    }
}