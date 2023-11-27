using blog_backend.DAO.Database;
using blog_backend.DAO.Model;
using blog_backend.Entity;
using blog_backend.Service;
using Microsoft.AspNetCore.Mvc;

namespace blog_backend.DAO.Controllers;

[ApiController]
[Route("api")]
public class TagController : Controller
{
    private readonly TagsService _tagsService;

    public TagController(TagsService tagsService)
    {
        _tagsService = tagsService;
    }
    
    [HttpPost("tags")]
    public async Task<ActionResult<Tag>> CreateTag([FromBody] CreateTagDTO tagDto)
    {
        try
        {
            return Ok(await _tagsService.CreateTag(tagDto));
        }
        catch (Exception e)
        {
            var statusCode = BadRequest().StatusCode.ToString();
            return BadRequest(new ErrorDTO { Message = e.Message, Status = statusCode });
        }
    }


    [HttpGet("tags")]
    public ActionResult<List<Tag>> GetTags()
    {
        try
        {
            return Ok(_tagsService.GetTags().Result);
        }
        catch (Exception e)
        {
            var statusCode = BadRequest().StatusCode.ToString();
            return BadRequest(new ErrorDTO { Message = e.Message, Status = statusCode });
        }
    }
}