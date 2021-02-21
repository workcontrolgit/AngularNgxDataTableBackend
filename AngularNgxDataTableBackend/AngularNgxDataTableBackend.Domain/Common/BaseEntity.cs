using System;

namespace AngularNgxDataTableBackend.Domain.Common
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }
    }
}