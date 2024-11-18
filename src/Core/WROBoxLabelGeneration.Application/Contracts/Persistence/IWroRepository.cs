using WROBoxLabelGeneration.Models;

namespace WROBoxLabelGeneration.Application.Contracts.Persistence
{
    public interface IWroRepository : IRepository<Wro>
    {
        Wro GetByIdWithDetails(int id, bool withPopulateBoxes = true);
    }
}
