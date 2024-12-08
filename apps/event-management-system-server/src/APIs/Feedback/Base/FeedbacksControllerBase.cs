using Microsoft.AspNetCore.Mvc;
using EventManagementSystem.APIs;
using EventManagementSystem.APIs.Dtos;
using EventManagementSystem.APIs.Errors;
using EventManagementSystem.APIs.Common;

namespace EventManagementSystem.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class FeedbacksControllerBase : ControllerBase
{
    protected readonly IFeedbacksService _service;
    public FeedbacksControllerBase(IFeedbacksService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Feedback
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Feedback>> CreateFeedback(FeedbackCreateInput input)
    {
        var feedback = await _service.CreateFeedback(input);

        return CreatedAtAction(nameof(Feedback), new { id = feedback.Id }, feedback);
    }

    /// <summary>
    /// Delete one Feedback
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteFeedback([FromRoute()]
    FeedbackWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteFeedback(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Feedbacks
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Feedback>>> Feedbacks([FromQuery()]
    FeedbackFindManyArgs filter)
    {
        return Ok(await _service.Feedbacks(filter));
    }

    /// <summary>
    /// Meta data about Feedback records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> FeedbacksMeta([FromQuery()]
    FeedbackFindManyArgs filter)
    {
        return Ok(await _service.FeedbacksMeta(filter));
    }

    /// <summary>
    /// Get one Feedback
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Feedback>> Feedback([FromRoute()]
    FeedbackWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Feedback(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Feedback
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateFeedback([FromRoute()]
    FeedbackWhereUniqueInput uniqueId, [FromQuery()]
    FeedbackUpdateInput feedbackUpdateDto)
    {
        try
        {
            await _service.UpdateFeedback(uniqueId, feedbackUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a event record for Feedback
    /// </summary>
    [HttpGet("{Id}/event")]
    public async Task<ActionResult<List<Event>>> GetEvent([FromRoute()]
    FeedbackWhereUniqueInput uniqueId)
    {
        var event = await _service.GetEvent(uniqueId);
            return Ok(event); }

    /// <summary>
    /// Get a user record for Feedback
    /// </summary>
    [HttpGet("{Id}/user")]
    public async Task<ActionResult<List<User>>> GetUser([FromRoute()]
    FeedbackWhereUniqueInput uniqueId)
    {
        var user = await _service.GetUser(uniqueId);
        return Ok(user);
    }

}
