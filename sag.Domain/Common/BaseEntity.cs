using sag.Domain.Common.Interfaces;

namespace sag.Domain.Common;

public abstract class BaseEntity: IEntity
{
    public Guid Id { get; set; }
}