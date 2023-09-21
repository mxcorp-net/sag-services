namespace sag.Domain.Common.Interfaces;

public interface IAuditableEntity : IEntity
{
    DateTime CreatedAt { get; set; }
    int CreatedBy { get; set; }
    DateTime UpdatedAt { get; set; }
    int UpdatedBy { get; set; }
}