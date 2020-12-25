using Core.Entities;
using Core.Interfaces.Infrastructure;
using Infrastructure.AppContext;
using Infrastructure.Repositories.Repositories.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Repositories.EFRepositories
{
    public class ApplicationUserRepository : BaseEfRepository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ApplicationUserRepository(
            AppDbContext appDbContext,
            UserManager<ApplicationUser> userManager
            ) : base(appDbContext)
        {
            _userManager = userManager;
        }

        public Task<ApplicationUser> FindByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        //public Task<ApplicationUser> FindByUserName(string userName)
        //{
        //    var user = await _userManager.FindByNameAsync(userName);
        //}
    }
}
