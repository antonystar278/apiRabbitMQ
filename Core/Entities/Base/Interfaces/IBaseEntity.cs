using System;

namespace Core.Entities.Base.Interfaces
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        DateTime CreationDate { get; set; }
    }

}
