using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RelationshipApp.Api.DTOs;
using RelationshipApp.Services.Interfaces;
using System.Security.Claims;

namespace RelationshipApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CouplesController : ControllerBase
{
    private readonly ICoupleService _coupleService;
    private readonly ILogger<CouplesController> _logger;

    public CouplesController(ICoupleService coupleService, ILogger<CouplesController> logger)
    {
        _coupleService = coupleService;
        _logger = logger;
    }

    private Guid GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.Parse(userIdClaim!);
    }

    [HttpPost]
    public async Task<ActionResult<CoupleResponse>> CreateCouple([FromBody] CreateCoupleRequest request)
    {
        try
        {
            var userId = GetCurrentUserId();
            var couple = await _coupleService.CreateCoupleAsync(userId, request.InitialCode);

            if (couple == null)
            {
                return BadRequest(new { message = "Unable to create couple. You may already be in a couple or the code is taken." });
            }

            var response = new CoupleResponse
            {
                Id = couple.Id,
                Code = couple.Code,
                CreatedAt = couple.CreatedAt,
                Members = couple.Members.Select(m => new CoupleMemberResponse
                {
                    UserId = m.UserId,
                    DisplayName = m.User?.DisplayName ?? string.Empty,
                    Role = m.Role,
                    JoinedAt = m.JoinedAt
                }).ToList()
            };

            return CreatedAtAction(nameof(GetCouple), new { id = couple.Id }, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating couple");
            return StatusCode(500, new { message = "An error occurred while creating the couple" });
        }
    }

    [HttpPost("{id}/join")]
    public async Task<ActionResult<CoupleResponse>> JoinCouple(Guid id, [FromBody] JoinCoupleRequest request)
    {
        try
        {
            var userId = GetCurrentUserId();
            var couple = await _coupleService.JoinCoupleAsync(userId, request.Code);

            if (couple == null)
            {
                return BadRequest(new { message = "Unable to join couple. The code may be invalid, the couple is full, or you're already in a couple." });
            }

            // Reload couple with members
            couple = await _coupleService.GetCoupleByIdAsync(couple.Id);

            var response = new CoupleResponse
            {
                Id = couple!.Id,
                Code = couple.Code,
                CreatedAt = couple.CreatedAt,
                Members = couple.Members.Select(m => new CoupleMemberResponse
                {
                    UserId = m.UserId,
                    DisplayName = m.User?.DisplayName ?? string.Empty,
                    Role = m.Role,
                    JoinedAt = m.JoinedAt
                }).ToList()
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error joining couple");
            return StatusCode(500, new { message = "An error occurred while joining the couple" });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CoupleResponse>> GetCouple(Guid id)
    {
        try
        {
            var userId = GetCurrentUserId();
            var couple = await _coupleService.GetCoupleByIdAsync(id);

            if (couple == null)
            {
                return NotFound(new { message = "Couple not found" });
            }

            // Check if user is member of this couple
            var isMember = await _coupleService.IsUserInCoupleAsync(userId, id);
            if (!isMember)
            {
                return Forbid();
            }

            var response = new CoupleResponse
            {
                Id = couple.Id,
                Code = couple.Code,
                CreatedAt = couple.CreatedAt,
                Members = couple.Members.Select(m => new CoupleMemberResponse
                {
                    UserId = m.UserId,
                    DisplayName = m.User?.DisplayName ?? string.Empty,
                    Role = m.Role,
                    JoinedAt = m.JoinedAt
                }).ToList()
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving couple");
            return StatusCode(500, new { message = "An error occurred while retrieving the couple" });
        }
    }

    [HttpGet("my-couple")]
    public async Task<ActionResult<CoupleResponse>> GetMyCouple()
    {
        try
        {
            var userId = GetCurrentUserId();
            var couple = await _coupleService.GetCoupleByUserIdAsync(userId);

            if (couple == null)
            {
                return NotFound(new { message = "You are not currently in a couple" });
            }

            var response = new CoupleResponse
            {
                Id = couple.Id,
                Code = couple.Code,
                CreatedAt = couple.CreatedAt,
                Members = couple.Members.Select(m => new CoupleMemberResponse
                {
                    UserId = m.UserId,
                    DisplayName = m.User?.DisplayName ?? string.Empty,
                    Role = m.Role,
                    JoinedAt = m.JoinedAt
                }).ToList()
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user's couple");
            return StatusCode(500, new { message = "An error occurred while retrieving your couple" });
        }
    }
}
