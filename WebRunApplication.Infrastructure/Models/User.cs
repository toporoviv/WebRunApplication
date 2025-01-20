using WebRunApplication.Domain.Enums;

namespace WebRunApplication.Infrastructure.Models;

public sealed record User
{
    public required string Login { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public required string Fullname { get; set; }
    public required int Age { get; set; }
    public required Gender Gender { get; set; }
    public required uint Weight { get; set; }
    public required uint Height { get; set; }
    public Role Role { get; set; }
}