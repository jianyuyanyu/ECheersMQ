using System;
using System.Threading.Tasks;
using Echeers.Mq.Data;
using Echeers.Mq.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Echeers.Mq.Controllers
{
    public class AppsController : Controller
    {
        private readonly MqDbContext _dbContext;
        private readonly UserManager<MqUser> _userManager;
        public AppsController(
            MqDbContext dbContext,
            UserManager<MqUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> MyApps()
        {
            var user = await GetCurrentUserAsync();
            return View(user.Apps);
        }

        public Task<MqUser> GetCurrentUserAsync()
        {
            return _dbContext.Users.Include(t => t.Apps).SingleOrDefaultAsync(t => t.UserName == User.Identity.Name);
        }
    }
}