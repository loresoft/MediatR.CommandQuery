using System;

namespace Tracker.WebService.Domain.Models;

public class AuditReadModel
    : EntityReadModel
{
    public DateTime Date { get; set; }

    public string? UserId { get; set; }

    public string? TaskId { get; set; }

    public string Content { get; set; } = null!;

    public string Username { get; set; } = null!;
}
