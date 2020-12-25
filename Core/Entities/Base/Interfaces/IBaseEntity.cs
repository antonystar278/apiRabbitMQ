using System;

namespace Core.Entities.Base.Interfaces
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        DateTime CreationDate { get; set; }
    }

}
