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

        public void Test()
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
                .First(wro => wro.RequestID == 94940);

            wro.SetBoxes();
            wro.UpdateShippingLabelUrlForBox(150758, "http://ucc.com");

            wro = _dbContext.Wros.Find(94940);
            
            if (wro == null)
            {
                throw new Exception();
            }

            wro.UpdateBoxLabelURL("https://111.com");
            _dbContext.SaveChanges();

            //var f = _dbContext.Wros.First(x => x.RequestID == 76302);
            //f.BoxLabelURL = "1111";
            //_dbContext.SaveChanges();
        }
    }
}
