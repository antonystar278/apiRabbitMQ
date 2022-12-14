using Core.Entities.Base.Interfaces;
using System;

namespace Core.Entities.Base
{
    public class BaseEntity : IBaseEntity
    {
        public int Id { get ; set; }
        public DateTime CreationDate { get; set; }
    }
}
