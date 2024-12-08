using EventManagementSystem.APIs;
using EventManagementSystem.APIs.Common;
using EventManagementSystem.APIs.Dtos;
using EventManagementSystem.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class UsersControllerBase : ControllerBase
{
    protected readonly IUsersService _service;

    public UsersControllerBase(IUsersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one User
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<User>> CreateUser(UserCreateInput input)
    {
        var user = await _service.CreateUser(input);

        return CreatedAtAction(nameof(User), new { id = user.Id }, user);
    }

    /// <summary>
    /// Delete one User
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteUser([FromRoute()] UserWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteUser(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Users
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<User>>> Users([FromQuery()] UserFindManyArgs filter)
    {
        return Ok(await _service.Users(filter));
    }

    /// <summary>
    /// Meta data about User records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> UsersMeta([FromQuery()] UserFindManyArgs filter)
    {
        return Ok(await _service.UsersMeta(filter));
    }

    /// <summary>
    /// Get one User
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<User>> User([FromRoute()] UserWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.User(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one User
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateUser(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromQuery()] UserUpdateInput userUpdateDto
    )
    {
        try
        {
            await _service.UpdateUser(uniqueId, userUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Feedbacks records to User
    /// </summary>
    [HttpPost("{Id}/feedbacks")]
    public async Task<ActionResult> ConnectFeedbacks(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromQuery()] FeedbackWhereUniqueInput[] feedbacksId
    )
    {
        try
        {
            await _service.ConnectFeedbacks(uniqueId, feedbacksId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Feedbacks records from User
    /// </summary>
    [HttpDelete("{Id}/feedbacks")]
    public async Task<ActionResult> DisconnectFeedbacks(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromBody()] FeedbackWhereUniqueInput[] feedbacksId
    )
    {
        try
        {
            await _service.DisconnectFeedbacks(uniqueId, feedbacksId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Feedbacks records for User
    /// </summary>
    [HttpGet("{Id}/feedbacks")]
    public async Task<ActionResult<List<Feedback>>> FindFeedbacks(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromQuery()] FeedbackFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindFeedbacks(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Feedbacks records for User
    /// </summary>
    [HttpPatch("{Id}/feedbacks")]
    public async Task<ActionResult> UpdateFeedbacks(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromBody()] FeedbackWhereUniqueInput[] feedbacksId
    )
    {
        try
        {
            await _service.UpdateFeedbacks(uniqueId, feedbacksId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Notifications records to User
    /// </summary>
    [HttpPost("{Id}/notifications")]
    public async Task<ActionResult> ConnectNotifications(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromQuery()] NotificationWhereUniqueInput[] notificationsId
    )
    {
        try
        {
            await _service.ConnectNotifications(uniqueId, notificationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Notifications records from User
    /// </summary>
    [HttpDelete("{Id}/notifications")]
    public async Task<ActionResult> DisconnectNotifications(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromBody()] NotificationWhereUniqueInput[] notificationsId
    )
    {
        try
        {
            await _service.DisconnectNotifications(uniqueId, notificationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Notifications records for User
    /// </summary>
    [HttpGet("{Id}/notifications")]
    public async Task<ActionResult<List<Notification>>> FindNotifications(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromQuery()] NotificationFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindNotifications(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Notifications records for User
    /// </summary>
    [HttpPatch("{Id}/notifications")]
    public async Task<ActionResult> UpdateNotifications(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromBody()] NotificationWhereUniqueInput[] notificationsId
    )
    {
        try
        {
            await _service.UpdateNotifications(uniqueId, notificationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple ParticipantRegistrations records to User
    /// </summary>
    [HttpPost("{Id}/participantRegistrations")]
    public async Task<ActionResult> ConnectParticipantRegistrations(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromQuery()] ParticipantRegistrationWhereUniqueInput[] participantRegistrationsId
    )
    {
        try
        {
            await _service.ConnectParticipantRegistrations(uniqueId, participantRegistrationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple ParticipantRegistrations records from User
    /// </summary>
    [HttpDelete("{Id}/participantRegistrations")]
    public async Task<ActionResult> DisconnectParticipantRegistrations(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromBody()] ParticipantRegistrationWhereUniqueInput[] participantRegistrationsId
    )
    {
        try
        {
            await _service.DisconnectParticipantRegistrations(uniqueId, participantRegistrationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple ParticipantRegistrations records for User
    /// </summary>
    [HttpGet("{Id}/participantRegistrations")]
    public async Task<ActionResult<List<ParticipantRegistration>>> FindParticipantRegistrations(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromQuery()] ParticipantRegistrationFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindParticipantRegistrations(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple ParticipantRegistrations records for User
    /// </summary>
    [HttpPatch("{Id}/participantRegistrations")]
    public async Task<ActionResult> UpdateParticipantRegistrations(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromBody()] ParticipantRegistrationWhereUniqueInput[] participantRegistrationsId
    )
    {
        try
        {
            await _service.UpdateParticipantRegistrations(uniqueId, participantRegistrationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
