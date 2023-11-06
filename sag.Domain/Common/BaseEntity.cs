using System.ComponentModel.DataAnnotations;
using sag.Domain.Common.Interfaces;

namespace sag.Domain.Common;

public abstract class BaseEntity: IEntity
{
    [Key] public Guid Id { get; set; }
}