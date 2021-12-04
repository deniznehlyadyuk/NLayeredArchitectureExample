using System;

namespace Core.Domain
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}