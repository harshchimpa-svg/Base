namespace Application.Dto.GetUserIdAndOrganizationIds;

public class GetUserIdAndOrganizationIdDto
{
    public bool IsAdmin { get; set; } = false;
    public int CounterId { get; set; }
    public string UserId { get; set; }
    public int? OrganizationId { get; set; }
    public int? UserOrganizationId { get; set; }
}
