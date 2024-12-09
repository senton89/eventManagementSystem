using EventManagementSystem.APIs;
using EventManagementSystem.APIs.Common;
using EventManagementSystem.APIs.Dtos;
using EventManagementSystem.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ParticipantRegistrationsControllerBase : ControllerBase
{
    protected readonly IParticipantRegistrationsService _service;

    public ParticipantRegistrationsControllerBase(IParticipantRegistrationsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one ParticipantRegistration
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<ParticipantRegistration>> CreateParticipantRegistration(
        ParticipantRegistrationCreateInput input
    )
    {
        var participantRegistration = await _service.CreateParticipantRegistration(input);

        return CreatedAtAction(
            nameof(ParticipantRegistration),
            new { id = participantRegistration.Id },
            participantRegistration
        );
    }

    /// <summary>
    /// Delete one ParticipantRegistration
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteParticipantRegistration(
        [FromRoute()] ParticipantRegistrationWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteParticipantRegistration(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many ParticipantRegistrations
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<ParticipantRegistration>>> ParticipantRegistrations(
        [FromQuery()] ParticipantRegistrationFindManyArgs filter
    )
    {
        return Ok(await _service.ParticipantRegistrations(filter));
    }

    /// <summary>
    /// Meta data about ParticipantRegistration records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ParticipantRegistrationsMeta(
        [FromQuery()] ParticipantRegistrationFindManyArgs filter
    )
    {
        return Ok(await _service.ParticipantRegistrationsMeta(filter));
    }

    /// <summary>
    /// Get one ParticipantRegistration
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<ParticipantRegistration>> ParticipantRegistration(
        [FromRoute()] ParticipantRegistrationWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.ParticipantRegistration(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one ParticipantRegistration
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateParticipantRegistration(
        [FromRoute()] ParticipantRegistrationWhereUniqueInput uniqueId,
        [FromQuery()] ParticipantRegistrationUpdateInput participantRegistrationUpdateDto
    )
    {
        try
        {
            await _service.UpdateParticipantRegistration(
                uniqueId,
                participantRegistrationUpdateDto
            );
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a event record for ParticipantRegistration
    /// </summary>
    [HttpGet("{Id}/event")]
    public async Task<ActionResult<List<Event>>> GetEvent(
        [FromRoute()] ParticipantRegistrationWhereUniqueInput uniqueId
    )
    {
        var eventDbModel = await _service.GetEvent(uniqueId);
        return Ok(eventDbModel);
    }

    /// <summary>
    /// Get a user record for ParticipantRegistration
    /// </summary>
    [HttpGet("{Id}/user")]
    public async Task<ActionResult<List<User>>> GetUser(
        [FromRoute()] ParticipantRegistrationWhereUniqueInput uniqueId
    )
    {
        var user = await _service.GetUser(uniqueId);
        return Ok(user);
    }
}
