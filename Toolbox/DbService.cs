using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Toolbox.Data;

namespace Toolbox
{
    public class DbService
    {
        private readonly ApplicationDbContext db;

        public DbService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ToolboxUser GetCurrentUser(ClaimsPrincipal user)
        {
            var id = user.FindFirst(ClaimTypes.NameIdentifier).Value;

            return db.Users
                .Include(o => o.UserWidgets)
                    .ThenInclude(o => o.Widget)
                .Single(o => o.Id == id);
        }

        public async Task<IEnumerable<Widget>> GetAllWidgets()
            => await db.Widgets.ToListAsync();

        public async Task AddWidgetToUser(string widgetId, string userId)
        {
            await db.UserWidgets.AddAsync(new UserWidget()
            {
                UserId = userId,
                WidgetId = widgetId
            });

            await db.SaveChangesAsync();
        }

        public async Task RemoveWidgetFromUser(string widgetId, string userId)
        {
            var entry = await db.UserWidgets.SingleAsync(o => o.WidgetId == widgetId && o.UserId == userId);
            db.UserWidgets.Remove(entry);
            await db.SaveChangesAsync();
        }
    }
}
