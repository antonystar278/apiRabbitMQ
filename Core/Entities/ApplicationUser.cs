using Core.Entities.Base.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class ApplicationUser : IdentityUser<int>, IBaseEntity
    {
        public DateTime CreationDate { get; set; }
        public IList<Operation> Operations { get; set; } = new List<Operation>();
    }
}
