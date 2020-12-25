using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Infrastructure
{
    public interface IApplicationUserRepository
    {
        Task<ApplicationUser> FindByUserName(string userName);
    }
}
