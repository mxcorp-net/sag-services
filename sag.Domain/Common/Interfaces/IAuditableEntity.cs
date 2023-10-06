namespace sag.Domain.Common.Interfaces;

public interface IAuditableEntity : IEntity
{
    DateTime CreatedAt { get; set; }
    Guid CreatedBy { get; set; }
    DateTime UpdatedAt { get; set; }
    Guid UpdatedBy { get; set; }
}