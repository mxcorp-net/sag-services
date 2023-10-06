using sag.Domain.Common.Interfaces;

namespace sag.Domain.Common;

public abstract class BaseAuditableEntity:  BaseEntity, IAuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
}