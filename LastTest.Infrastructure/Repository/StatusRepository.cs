using LastTest.DataAccess.Repository;
using LastTest.Domain.Models.DataModels;
using LastTest.Infrastructure.Data;

namespace LastTest.Infrastructure.Repository
{
    public class StatusRepository : Repository<ReservationStatus>, IRepository<ReservationStatus>
    {

        public StatusRepository(ApplicationDbContext context)
            : base(context)
        {

        }

    }
}
