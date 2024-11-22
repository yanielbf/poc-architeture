using Microsoft.EntityFrameworkCore;
using WROBoxLabelGeneration.Application.Contracts.Persistence;
using WROBoxLabelGeneration.Models;
using WROBoxLabelGeneration.Persistence.DbContexts;

namespace WROBoxLabelGeneration.Persistence.Repositories
{
    public class WroRepository : Repository<Wro>, IWroRepository
    {
        public WroRepository(ShipBobLiveDbContext dbContext) : base(dbContext) {}

        public Wro GetByIdWithDetails(int id, bool withPopulateBoxes = true)
        {
            var wro = _dbContext.Wros
                .AsNoTracking()
                .AsSplitQuery()
                .Include(wro => wro.User)
                .Include(wro => wro.FulfillmentCenter)
                    .ThenInclude(fc => fc.City)
                .Include(wro => wro.WroOriginAddress)
                .Include(wro => wro.WroInventoryDetails)
                    .ThenInclude(wid => wid.Inventory)
                .Include(wro => wro.WroInventoryDetails)
                    .ThenInclude(wid => wid.WroInventoryPackagings)
                        .ThenInclude(wip => wip.WroPackagingDetail)
                .First(wro => wro.RequestID == id);

            if (withPopulateBoxes)
            {
                wro.SetBoxes();
            }

            return wro;
        }
    }
}
