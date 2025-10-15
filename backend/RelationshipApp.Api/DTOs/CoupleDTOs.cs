namespace RelationshipApp.Api.DTOs;

public class CreateCoupleRequest
{
    public string? InitialCode { get; set; }
}

public class JoinCoupleRequest
{
    public string Code { get; set; } = string.Empty;
}

public class CoupleResponse
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<CoupleMemberResponse> Members { get; set; } = new();
}

public class CoupleMemberResponse
{
    public Guid UserId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime JoinedAt { get; set; }
}
